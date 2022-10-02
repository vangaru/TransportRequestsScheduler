using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Interactor.Tests.Utils
{
    internal static class RequestFactoryMock
    {
        private const int MinSeatsCount = 1;
        private const int MaxSeatsCount = 3;

        private static readonly Random _random = new();

        public static Request CreateRequest(string busStopName)
        {
            int busStopIndex = ConfigurationMock.BusStopNames.IndexOf(busStopName);

            return new Request
            {
                SourceBusStopName = busStopName,
                DestinationBusStopName = busStopIndex == ConfigurationMock.BusStopNames.Count - 1
                                            ? ConfigurationMock.BusStopNames[busStopIndex - 1]
                                            : ConfigurationMock.BusStopNames[busStopIndex + 1],
                DateTime = DateTime.Now,
                SeatsCount = _random.Next(MinSeatsCount, MaxSeatsCount + 1)
            };
        }
    }
}
