using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Worker.Scheduler
{
    public interface IRequestsScheduler
    {
        public Task ScheduleAsync(Request request);
    }
}
