using BSTU.RequestsScheduler.Interactor.Configuration;
using BSTU.RequestsScheduler.Interactor.Models;
using BSTU.RequestsScheduler.Interactor.Presentators;
using BSTU.RequestsScheduler.Worker.Loggers;
using BSTU.RequestsScheduler.Worker.Scheduler;

namespace Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IStatisticsLogger _statisticsLogger;
        private readonly IRequestsPresentator _requestsPresentator;
        private readonly IRequestsScheduler _requestsScheduler;

        private IEnumerable<Queue<Request>>? _requestQueuesForCurrentTimePeriod;

        public Worker(
            IRequestsPresentator requestsPresentator, 
            ILogger<Worker> logger, 
            IStatisticsLogger statisticsLogger, 
            IRequestsScheduler requestsScheduler)
        {
            _logger = logger;
            _statisticsLogger = statisticsLogger;
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
            if (requests.TryPeek(out Request? requestInfo))
            {
                if (requestInfo != null)
                {
                    var submittedRequestsCount = 0;
                    int initialRequestsCount = requests.Count;
                    TimeSpan firstRequestTimeOfDay = requestInfo.DateTime.TimeOfDay;

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            if (requests.TryDequeue(out Request? request))
                            {
                                if (request != null)
                                {
                                    _requestsScheduler.Schedule(request);
                                    submittedRequestsCount++;
                                    requestInfo = request;
                                }
                            }
                            else
                            {
                                await _statisticsLogger.LogAsync(firstRequestTimeOfDay, requestInfo.DateTime.TimeOfDay, 
                                    requestInfo.SourceBusStopName, initialRequestsCount, submittedRequestsCount);
                                requests = _requestsPresentator.GetRequestQueueForBusStop(requestInfo.SourceBusStopName);
                                initialRequestsCount = requests.Count;
                                submittedRequestsCount = 0;
                            }
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
}