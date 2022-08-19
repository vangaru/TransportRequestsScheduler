using BSTU.RequestsScheduler.Interactor.Interactors;
using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Interactor.Presentators
{
    public class RequestsPresentator : IRequestsPresentator
    {
        public RequestsPresentator(IRequestsInteractor requestsInteractor)
        {

        }

        public IEnumerable<Queue<Request>> RequestQueues => throw new NotImplementedException();
    }
}