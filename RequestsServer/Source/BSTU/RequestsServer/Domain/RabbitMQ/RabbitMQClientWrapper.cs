using System.Text;
using BSTU.RequestsServer.Domain.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace BSTU.RequestsServer.Domain.RabbitMQ
{
    public class RabbitMQClientWrapper : IRabbitMQClientWrapper
    {
        private const int DefaultRabbitMQPort = 5432;

        private readonly RabbitMQConfiguration _rabbitMQConfiguration;

        public RabbitMQClientWrapper(IOptions<RabbitMQConfiguration> rabbitMQConfiguraiton)
        {
            _rabbitMQConfiguration = rabbitMQConfiguraiton.Value;
        }

        public void Send(Request request)
        {
            using IConnection connection = ConnectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue: _rabbitMQConfiguration.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            IBasicProperties channelProperties = channel.CreateBasicProperties();
            byte[] messageBody = Encoding.UTF8.GetBytes(request.ToString());
            channel.BasicPublish(exchange: "", routingKey: _rabbitMQConfiguration.QueueName, 
                basicProperties: channelProperties, body: messageBody);
        }

        private ConnectionFactory ConnectionFactory
        {
            get
            {
                return new ConnectionFactory
                {
                    HostName = _rabbitMQConfiguration.HostName,
                    VirtualHost = _rabbitMQConfiguration.VirtualHost,
                    UserName = _rabbitMQConfiguration.UserName,
                    Password = _rabbitMQConfiguration.Password,
                    Port = _rabbitMQConfiguration.Port ?? DefaultRabbitMQPort
                };
            }
        }
    }
}