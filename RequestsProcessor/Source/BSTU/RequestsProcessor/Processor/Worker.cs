using BSTU.RequestsProcessor.Domain.RabbitMQ;

namespace BSTU.RequestsProcessor.Processor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageJobsHandler _messageJobsHandler;

        public Worker(ILogger<Worker> logger, IMessageJobsHandler messageJobsHandler)
        {
            _logger = logger;
            _messageJobsHandler = messageJobsHandler;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _messageJobsHandler.Setup();
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                stoppingToken.ThrowIfCancellationRequested();
                _messageJobsHandler.Subscribe();
                await Task.CompletedTask;
            }
            catch (AggregateException exception)
            {
                _logger.LogError(exception, exception.Message);
                foreach (Exception innerException in exception.InnerExceptions)
                {
                    _logger.LogError(innerException, innerException.Message);
                }
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }


        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _messageJobsHandler.Stop();
            return base.StopAsync(cancellationToken);
        }
    }
}