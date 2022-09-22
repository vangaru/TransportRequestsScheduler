using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Interactor.Interactors
{
    public interface IRequestsInteractor
    {
        public IEnumerable<Request> Requests { get; }

        public IEnumerable<Request> GetRequestsFor(string busStopName);
    }
}
