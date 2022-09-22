using BSTU.RequestsScheduler.Interactor.Interactors;
using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Interactor.Presentators
{
    public class RequestsPresentator : IRequestsPresentator
    {
        private readonly IRequestsInteractor _requestsInteractor;

        public RequestsPresentator(IRequestsInteractor requestsInteractor)
        {
            _requestsInteractor = requestsInteractor;
        }

        public IEnumerable<Queue<Request>> RequestQueues
        {
            get
            {
                IEnumerable<Request> requests = _requestsInteractor.Requests;

                var busStopNames = new HashSet<string>(requests.Select(request => request.SourceBusStopName));

                var requestQueues = new List<Queue<Request>>();
                
                foreach (string busStopName in busStopNames)
                {
                    var busStopRequests = requests
                        .Where(request => request.SourceBusStopName.Equals(busStopName, StringComparison.InvariantCultureIgnoreCase))
                        .ToList();
                    busStopRequests.Sort();
                    var requestQueue = new Queue<Request>(busStopRequests);
                    requestQueues.Add(requestQueue);
                }

                return requestQueues;
            }
        }

        public Queue<Request> GetRequestQueueForBusStop(string busStopName)
        {
            List<Request> requests = _requestsInteractor.GetRequestsFor(busStopName).ToList();
            requests.Sort();
            return new Queue<Request>(requests);
        }
    }
}