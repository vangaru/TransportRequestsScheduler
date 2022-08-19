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
                    var requestQueue = new Queue<Request>(requests
                        .Where(request => request.SourceBusStopName.Equals(busStopName, StringComparison.InvariantCultureIgnoreCase)));
                    requestQueues.Add(requestQueue);
                }

                return requestQueues;
            }
        }
    }
}