using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Interactor.Providers
{
    public class RequestsCountProvider : IRequestsCountProvider
    {
        private readonly IRequestConfigurationProxy _configuration;

        public RequestsCountProvider(IRequestConfigurationProxy configuration)
        {
            _configuration = configuration;
        }

        public int GetRequestsCountForCurrentPeriod(string busStopName)
        {
            BusStopConfiguration busStop = GetBusStopByName(busStopName);
            TimePeriod currentTimePeriod = GetCurrentTimePeriodForBusStop(busStop);
            int requestsCount = GetRequestsCount(busStop, currentTimePeriod);
            return requestsCount;
        }

        private BusStopConfiguration GetBusStopByName(string busStopName)
        {
            BusStopConfiguration busStop = _configuration.Configuration
                .First(stop => stop.Name.Equals(busStopName, StringComparison.InvariantCultureIgnoreCase));

            return busStop;
        }

        private TimePeriod GetCurrentTimePeriodForBusStop(BusStopConfiguration busStop)
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            TimePeriod currentTimePeriod = busStop.TimePeriods
                .First(period => period.From.TimeOfDay <= currentTime && period.To.TimeOfDay >= currentTime);

            return currentTimePeriod;
        }

        private int GetRequestsCount(BusStopConfiguration busStop, TimePeriod timePeriod)
        {
            double requestsCount = Math.Ceiling(busStop.DailyRequestsCount * timePeriod.RequestsCountCoefficient);
            return Convert.ToInt32(requestsCount);
        }
    }
}