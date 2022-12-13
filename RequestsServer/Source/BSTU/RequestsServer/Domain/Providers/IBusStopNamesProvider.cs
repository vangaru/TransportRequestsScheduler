namespace BSTU.RequestsServer.Domain.Providers
{
    public interface IBusStopNamesProvider
    {
        public List<string> BusStopNames { get; }
    }
}