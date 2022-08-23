namespace BSTU.RequestsScheduler.Configuration.Validators
{
    internal static class ValidationMessage
    {
        private const string ValidationFailedMessage = "Validation Failed:";
        
        private const string DailyRequestsCountLessThanOneMessage = 
            "Daily requests count cannot be less than 1. " +
            "Got {0} for {1} bus stop.";
        
        private const string BusStopNamesNotUniqueMessage = 
            "Every bus stop name must be unique. " +
            "{0} duplicates found.";
        
        private const string BusStopRequestsCountCoefficientNotEqualOneMessage = 
            "Bus stop requests count coefficient must be equal 1. " +
            "Got {0} for {1} bus stop";

        private const string BusStopsCountLessThanTwoMessage =
            "At least 2 bus stops required. Got {0} bus stops.";

        private const string TimePeriodsCrossedMessage =
            "Time Periods for {0} bus stop are crossed";

        private const string TimePeriodsDoNotCover24hInterval =
            "Time Periods for {0} bus stop do not cover 24 hours interval";

        public static string GetDailyRequestsCountLessThanOneMessage(int requestsCount, string busStopName)
        {
            return GetValidationMessage(DailyRequestsCountLessThanOneMessage, requestsCount, busStopName);
        }

        public static string GetBusStopNamesNotUniqueMessage(int duplicatesCount)
        {
            return GetValidationMessage(BusStopNamesNotUniqueMessage, duplicatesCount);
        }

        public static string GetBusStopRequestsCountCoefficientNotEqualOneMessage(float coefficient, string busStopName)
        {
            return GetValidationMessage(BusStopRequestsCountCoefficientNotEqualOneMessage, coefficient, busStopName);
        }

        public static string GetBusStopsCountLessThanTwoMessage(int busStopsCount)
        {
            return GetValidationMessage(BusStopsCountLessThanTwoMessage, busStopsCount);
        }

        public static string GetTimePeriodsCrossedMessage(string busStopName)
        {
            return GetValidationMessage(TimePeriodsCrossedMessage, busStopName);
        }

        public static string GetTimePeriodsDoNotCover24hIntervalMessage(string busStopName)
        {
            return GetValidationMessage(TimePeriodsDoNotCover24hInterval, busStopName);
        }

        private static string GetValidationMessage(string message, params object[] parameters)
        {
            string validationMessage = string.Format(message, parameters);
            return $"{ValidationFailedMessage} {validationMessage}";
        }
    }
}