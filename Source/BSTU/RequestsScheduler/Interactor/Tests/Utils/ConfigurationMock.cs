using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Interactor.Tests.Utils
{
    internal static class ConfigurationMock
    {
        private const int MinDailyRequestsCount = 30;
        private const int MaxDailyRequestsCount = 500;

        private static readonly Random _random = new();
        
        public static readonly List<string> BusStopNames = new()
        {
            "Stop1",
            "Stop2",
            "Stop3",
            "Stop4",
            "Stop5",
            "Stop6",
            "Stop7",
            "Stop8",
            "Stop9",
            "Stop10"
        };

        public static IEnumerable<BusStopConfiguration> Configuration
        {
            get
            {
                var busStopConfiguration = new List<BusStopConfiguration>();

                foreach (string name in BusStopNames)
                {
                    busStopConfiguration.Add(GenerateBusStopConfiguration(name));
                }
                return busStopConfiguration;
            }
        }

        private static BusStopConfiguration GenerateBusStopConfiguration(string name) 
        {
            var busStopConfiguration = new BusStopConfiguration
            {
                Name = name,
                DailyRequestsCount = _random.Next(MinDailyRequestsCount, MaxDailyRequestsCount),
                TimePeriods = GenerateTimePeriods()
            };

            return busStopConfiguration;
        }

        private static List<TimePeriod> GenerateTimePeriods() 
        {
            var timePeriods = new List<TimePeriod>
            {
                new TimePeriod
                {
                    From = DateTime.Parse("00:00:00"),
                    To = DateTime.Parse("06:59:59"),
                    RequestsCountCoefficient = 0.05f
                },
                new TimePeriod
                {
                    From = DateTime.Parse("07:00:00"),
                    To = DateTime.Parse("10:59:59"),
                    RequestsCountCoefficient = 0.3f
                },
                new TimePeriod
                {
                    From = DateTime.Parse("11:00:00"),
                    To = DateTime.Parse("16:59:59"),
                    RequestsCountCoefficient = 0.2f
                },
                new TimePeriod
                {
                    From = DateTime.Parse("17:00:00"),
                    To = DateTime.Parse("19:59:59"),
                    RequestsCountCoefficient = 0.35f
                },
                new TimePeriod
                {
                    From = DateTime.Parse("20:00:00"),
                    To = DateTime.Parse("23:59:59"),
                    RequestsCountCoefficient = 0.1f
                }
            };

            return timePeriods;
        }

        public static IEnumerable<BusStopConfiguration> EmptyConfiguration => new List<BusStopConfiguration>();
    }
}