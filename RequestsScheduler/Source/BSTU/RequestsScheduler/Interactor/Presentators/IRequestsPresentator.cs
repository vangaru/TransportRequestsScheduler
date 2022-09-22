using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Interactor.Presentators
{
    public interface IRequestsPresentator
    {
        public IEnumerable<Queue<Request>> RequestQueues { get; }

        public Queue<Request> GetRequestQueueForBusStop(string busStopName);
    }
}