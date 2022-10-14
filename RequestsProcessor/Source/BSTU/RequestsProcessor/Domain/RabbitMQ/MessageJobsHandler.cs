using System.Text;
using BSTU.RequestsProcessor.Domain.Processors;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BSTU.RequestsProcessor.Domain.RabbitMQ
{
    public sealed class MessageJobsHandler : IMessageJobsHandler
    {
        private const int DefaultRabbitMQPort = 5432;

        private readonly RabbitMQConfiguration _rabbitMQConfiguration;
        private readonly IRequestsProcessor _requestsProcessor;
        private readonly ILogger<MessageJobsHandler> _logger;

        private IConnection? _connection;
        private IModel? _channel;
        private bool _disposed;

        public MessageJobsHandler(
            IOptions<RabbitMQConfiguration> rabbitMQOptions, 
            IRequestsProcessor requestsProcessor,
            ILogger<MessageJobsHandler> logger)
        {
            _rabbitMQConfiguration = rabbitMQOptions.Value;
            _requestsProcessor = requestsProcessor;
            _logger = logger;
        }

        public void Setup()
        {
            var connectionFactory = new ConnectionFactory
            {
                UserName = _rabbitMQConfiguration.UserName,
                Password = _rabbitMQConfiguration.Password,
                VirtualHost = _rabbitMQConfiguration.VirtualHost,
                HostName = _rabbitMQConfiguration.HostName,
                Port = _rabbitMQConfiguration.Port ?? DefaultRabbitMQPort,
                DispatchConsumersAsync = true
            };

            Connection = connectionFactory.CreateConnection();
            
            Channel.QueueDeclare(_rabbitMQConfiguration.QueueName, durable: true, 
                exclusive: false, autoDelete: false, arguments: null);

            Channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }

        public void Subscribe()
        {
            var consumer = new AsyncEventingBasicConsumer(Channel);
            consumer.Received += async (_, eventArgs) => await HandleMessageAsync(eventArgs);
            Channel.BasicConsume(queue: _rabbitMQConfiguration.QueueName, autoAck: false, consumer);
        }

        private async Task HandleMessageAsync(BasicDeliverEventArgs eventArgs)
        {
            byte[] messageBody = eventArgs.Body.ToArray();
            string requestContents = Encoding.UTF8.GetString(messageBody);
            await _requestsProcessor.ProcessAsync(requestContents)
                .ContinueWith(task => FinishHandleProcess(task, eventArgs, requestContents));
        }

        private void FinishHandleProcess(Task handleProcessTask, BasicDeliverEventArgs deliveryArgs, string requestContents)
        {
            if (handleProcessTask.IsFaulted)
            {
                FinishFaultedHandleProcess(handleProcessTask.Exception, deliveryArgs, requestContents);
            }
            else
            {
                FinishSuccessfulHandleProcess(deliveryArgs, requestContents);
            }
        }

        private void FinishFaultedHandleProcess(AggregateException? exception, BasicDeliverEventArgs deliveryArgs, string requestContents)
        {
            _logger.LogInformation($"Failed to process request:\n {requestContents}");

            if (exception != null)
            {
                _logger.LogError(exception, exception.Message);
                foreach (Exception innerException in exception.InnerExceptions) 
                {
                    _logger.LogError(innerException, innerException.Message);
                }
            }

            Channel.BasicNack(deliveryArgs.DeliveryTag, multiple: false, requeue: false);
        }

        private void FinishSuccessfulHandleProcess(BasicDeliverEventArgs deliveryArgs, string requestContents)
        {
            Channel.BasicAck(deliveryArgs.DeliveryTag, multiple: false);
            _logger.LogInformation($"Successfully processed request:\n {requestContents}");
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Stop();
            }
        }

        public void Stop()
        {
            if (!_disposed)
            {
                Channel.Close();
                Connection.Close();
                _disposed = true;
            }
        }

        private IConnection Connection
        {
            get => _connection 
                ?? throw new ApplicationException($"Failed to establish connection with RabbitMQ service.");
            set => _connection = value;
        }

        private IModel Channel
        {
            get
            {
                if (_channel == null)
                {
                    _channel = Connection.CreateModel();
                }

                return _channel;
            }

            set => _channel = value;
        }
    }
}