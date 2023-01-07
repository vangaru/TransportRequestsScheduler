using BSTU.RequestsScheduler.Interactor.Models;
using BSTU.RequestsScheduler.Interactor.Presentators;
using BSTU.RequestsScheduler.Worker.Scheduler;

namespace BSTU.RequestsScheduler.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRequestsPresentator _requestsPresentator;
        private readonly IRequestsScheduler _requestsScheduler;

        private IEnumerable<Queue<Request>>? _requestQueuesForCurrentTimePeriod;

        public Worker(
            IRequestsPresentator requestsPresentator, 
            ILogger<Worker> logger, 
            IRequestsScheduler requestsScheduler)
        {
            _logger = logger;
            _requestsPresentator = requestsPresentator;
            _requestsScheduler = requestsScheduler;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _requestQueuesForCurrentTimePeriod = _requestsPresentator.RequestQueues;

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_requestQueuesForCurrentTimePeriod == null)
            {
                throw new ApplicationException("Failed to retrieve requests.");
            }

            var parallelOptions = new ParallelOptions 
            { 
                MaxDegreeOfParallelism = _requestQueuesForCurrentTimePeriod.Count() 
            };

            await Parallel.ForEachAsync(_requestQueuesForCurrentTimePeriod, parallelOptions, async (requests, stopppingToken) =>
            {
                await ScheduleRequestsAsync(requests, stoppingToken);
            });
        }

        private async Task ScheduleRequestsAsync(Queue<Request> requests, CancellationToken stoppingToken)
        {
            if (requests.TryPeek(out Request? _))
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        if (requests.TryDequeue(out Request? request))
                        {
                            await _requestsScheduler.ScheduleAsync(request);
                        }
                        else
                        {
                            _logger.LogWarning("Failed to retrieve request from the queue.");
                        }
                    }
                    catch (AggregateException e)
                    {
                        _logger.LogError(e.InnerException, e.InnerException?.Message);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, e.Message);
                    }
                }
            }
        }
    }
}