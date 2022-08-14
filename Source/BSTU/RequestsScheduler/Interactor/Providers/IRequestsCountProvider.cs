namespace BSTU.RequestsScheduler.Interactor.Providers
{
    public interface IRequestsCountProvider
    {
        public int GetRequestsCountForCurrentPeriod(string busStopName);
    }
}
