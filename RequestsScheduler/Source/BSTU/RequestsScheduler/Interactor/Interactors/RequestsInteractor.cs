using System.Collections.Concurrent;
using BSTU.RequestsScheduler.Interactor.Configuration;
using BSTU.RequestsScheduler.Interactor.Factories;
using BSTU.RequestsScheduler.Interactor.Models;
using BSTU.RequestsScheduler.Interactor.Providers;

namespace BSTU.RequestsScheduler.Interactor.Interactors
{
    public class RequestsInteractor : IRequestsInteractor
    {
        private readonly IRequestConfigurationProxy _configuration;
        private readonly IRequestsCountProvider _requestsCountProvider;
        private readonly IRequestFactory _requestFactory;

        public RequestsInteractor(
            IRequestConfigurationProxy configuration,
            IRequestsCountProvider requestsCountProvider,
            IRequestFactory requestFactory)
        {
            _configuration = configuration;
            _requestsCountProvider = requestsCountProvider;
            _requestFactory = requestFactory;
        }

        public IEnumerable<Request> Requests
        {
            get
            {
                var requests = new BlockingCollection<Request>();
                Parallel.ForEach(_configuration.Configuration, busStop =>
                {
                    IEnumerable<Request> busStopRequests = GetRequestsFor(busStop.Name);
                    foreach (Request request in busStopRequests)
                    {
                        requests.Add(request);
                    }
                });
                requests.CompleteAdding();
                return requests;
            }
        }

        public IEnumerable<Request> GetRequestsFor(string busStopName)
        {
            int requestsCount = _requestsCountProvider.GetRequestsCountForCurrentPeriod(busStopName);
            var requests = new List<Request>(requestsCount);
            for (var _ = 0; _ < requestsCount; _++)
            {
                Request request = _requestFactory.Create(busStopName);
                requests.Add(request);
            }

            return requests;
        }
    }
}