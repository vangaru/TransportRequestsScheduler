using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Worker.Loggers
{
    public interface IStatisticsLogger
    {
        public Task LogAsync(TimeSpan from, TimeSpan to, string busStopName, 
            int expectedRequestsCount, int actualSubmittedRequestsCount);
    }
}
