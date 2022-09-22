using BSTU.RequestsScheduler.Interactor.Interactors;
using BSTU.RequestsScheduler.Interactor.Models;
using BSTU.RequestsScheduler.Interactor.Presentators;
using BSTU.RequestsScheduler.Interactor.Tests.Utils;
using Moq;

namespace BSTU.RequestsScheduler.Interactor.Tests.Presentators
{
    public class RequestsPresentatorTests
    {
        private readonly IRequestsPresentator _requestsPresentator;
        private readonly Mock<IRequestsInteractor> _requestsInteractor;

        public RequestsPresentatorTests()
        {
            _requestsInteractor = new Mock<IRequestsInteractor>();
            _requestsInteractor.Setup(interactor => interactor.Requests).Returns(RequestsInteractorMock.Requests);
            _requestsPresentator = new RequestsPresentator(_requestsInteractor.Object);
        }

        [Fact]
        public void RequestQueues_ListOfRequests_EveryBusStopNameIsMappedToASeparateQueue()
        {
            IEnumerable<Queue<Request>> requestQueues = _requestsPresentator.RequestQueues;
            foreach (Queue<Request> requestQueue in requestQueues)
            {
                Assert.NotEmpty(requestQueue);
                Request request = requestQueue.Peek();
                Assert.All(requestQueue, request => request.SourceBusStopName.Equals(request.SourceBusStopName));
                IEnumerable<Request> requestsFromInteractorWithSameBusStopName = _requestsInteractor.Object.Requests
                    .Where(req => req.SourceBusStopName.Equals(request.SourceBusStopName));
                Assert.Equal(requestsFromInteractorWithSameBusStopName.Count(), requestQueue.Count);
            }
        }
    }
}