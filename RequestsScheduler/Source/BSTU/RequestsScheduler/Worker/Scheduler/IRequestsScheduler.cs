using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Worker.Scheduler
{
    public interface IRequestsScheduler
    {
        public void Schedule(Request request);
    }
}
