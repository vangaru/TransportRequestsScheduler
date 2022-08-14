using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Interactor.Presentators
{
    public class RequestsPresentator : IRequestsPresentator
    {
        public IEnumerable<Queue<Request>> RequestQueues => throw new NotImplementedException();
    }
}
