namespace BSTU.RequestsScheduler.Interactor.Configuration
{
    public interface IRequestConfigurationProxy
    {
        public IEnumerable<BusStopConfiguration> Configuration { get; }
    }
}