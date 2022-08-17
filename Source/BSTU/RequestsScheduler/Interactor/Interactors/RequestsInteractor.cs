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
                    int requestsCount = _requestsCountProvider.GetRequestsCountForCurrentPeriod(busStop.Name);
                    for (var i = 0; i < requestsCount; i++)
                    {
                        Request request = _requestFactory.Create(busStop.Name);
                        requests.Add(request);
                    }
                });
                requests.CompleteAdding();
                return requests;
            }
        }
    }
}