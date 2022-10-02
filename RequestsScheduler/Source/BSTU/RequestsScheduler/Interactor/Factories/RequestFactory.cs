using BSTU.RequestsScheduler.Interactor.Configuration;
using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Interactor.Factories
{
    public class RequestFactory : IRequestFactory
    {
        private const int MinSeatsCount = 1;
        private const int MaxSeatsCount = 3;

        private readonly IRequestConfigurationProxy _configurationProxy;
        private readonly Random _random = new();

        public RequestFactory(IRequestConfigurationProxy configuration)
        {
            _configurationProxy = configuration;
        }

        public Request Create(string busStopName)
        {
            var request = new Request
            {
                SourceBusStopName = busStopName,
                DestinationBusStopName = GetDestinationBusStopName(busStopName),
                SeatsCount = _random.Next(MinSeatsCount, MaxSeatsCount + 1),
                DateTime = GetDateTime(busStopName)
            };

            return request;
        }

        private string GetDestinationBusStopName(string sourceBusStopName)
        {
            int destinationBusStopIndex = GetDestinationBusStopIndex(sourceBusStopName);
            return _configurationProxy.Configuration.ElementAt(destinationBusStopIndex).Name;
        }

        private int GetDestinationBusStopIndex(string sourceBusStopName)
        {
            int randomBusStopNameIndex = _random.Next(_configurationProxy.Configuration.Count());
            return BusStopsEqual(sourceBusStopName, randomBusStopNameIndex)
                ? randomBusStopNameIndex >= _configurationProxy.Configuration.Count()
                    ? --randomBusStopNameIndex
                    : ++randomBusStopNameIndex
                : randomBusStopNameIndex;
        }

        private bool BusStopsEqual(string sourceBusStopName, int destinationBusStopIndex)
        {
            return _configurationProxy.Configuration.ToList()
                .IndexOf(GetBusStopByName(sourceBusStopName)) == destinationBusStopIndex;
        }

        private DateTime GetDateTime(string busStopName)
        {
            BusStopConfiguration busStop = GetBusStopByName(busStopName);
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            TimePeriod currentTimePeriod = busStop.TimePeriods.First(timePeriod => timePeriod.From.TimeOfDay <= currentTime && timePeriod.To.TimeOfDay >= currentTime);
            double seconds = (currentTimePeriod.To.TimeOfDay - currentTimePeriod.From.TimeOfDay).TotalSeconds;
            var timePeriod = currentTimePeriod.From.AddSeconds(_random.Next((int)seconds));
            return timePeriod;
        }

        private BusStopConfiguration GetBusStopByName(string busStopName)
        {
            return _configurationProxy.Configuration.First(busStop => string.Compare(busStopName, busStop.Name) == 0);
        }
    }
}