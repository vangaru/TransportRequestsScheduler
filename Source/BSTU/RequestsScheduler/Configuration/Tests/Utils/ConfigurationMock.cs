using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Tests.Utils
{
    internal static class ConfigurationMock
    {
        private static readonly Random _random = new();

        public const int MinDailyRequestsCount = 30;
        public const int MaxDailyRequestsCount = 500;

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

        public static IEnumerable<BusStopConfiguration> ValidConfiguration => GenerateConfiguration(ValidTimePeriods, GenerateBusStopConfiguration);

        public static IEnumerable<BusStopConfiguration> ConfigurationWithEmptyTimePeriods => GenerateConfiguration(EmptyTimePeriods, GenerateBusStopConfiguration);

        public static IEnumerable<BusStopConfiguration> ConfigurationWithRepeatedNames => ValidConfiguration.Concat(ValidConfiguration);

        public static IEnumerable<BusStopConfiguration> EmptyConfiguration => new List<BusStopConfiguration>();

        public static IEnumerable<BusStopConfiguration> ConfigurationWithCrossTimePeriods => GenerateConfiguration(CrossTimePeriods, GenerateBusStopConfiguration);

        public static IEnumerable<BusStopConfiguration> ConfigurationWithTimePeriodsWhichDontCoverDaily24hInterval => GenerateConfiguration(TimePeriodsWhichDontCoverDaily24hInterval, GenerateBusStopConfiguration);

        public static IEnumerable<BusStopConfiguration> ConfigurationWithSummaryRequestsCoefficientMoreThan1 => GenerateConfiguration(TimePeriodsWithSummaryRequestsCountCoefficientMoreThan1, GenerateBusStopConfiguration);

        public static IEnumerable<BusStopConfiguration> ConfigurationWithDailyRequestsCountLessThan1 => GenerateConfiguration(ValidTimePeriods, GenerateBusStopConfigurationWithRequestsCountLessThan1);

        private static IEnumerable<BusStopConfiguration> GenerateConfiguration(IEnumerable<TimePeriod> timePeriods, 
            Func<string, List<TimePeriod>, BusStopConfiguration> busStopConfigurationGenerator)
        {
            var busStopConfiguration = new List<BusStopConfiguration>();

            foreach (string name in BusStopNames)
            {
                busStopConfiguration.Add(busStopConfigurationGenerator(name, ValidTimePeriods));
            }

            return busStopConfiguration;
        }

        private static BusStopConfiguration GenerateBusStopConfiguration(string name, List<TimePeriod> timePeriods)
        {
            var busStopConfiguration = new BusStopConfiguration
            {
                Name = name,
                DailyRequestsCount = _random.Next(MinDailyRequestsCount, MaxDailyRequestsCount),
                TimePeriods = ValidTimePeriods
            };

            return busStopConfiguration;
        }

        private static BusStopConfiguration GenerateBusStopConfigurationWithRequestsCountLessThan1(string name, List<TimePeriod> timePeriods)
        {
            var busStopConfiguration = new BusStopConfiguration
            {
                Name = name,
                DailyRequestsCount = 0,
                TimePeriods = ValidTimePeriods
            };

            return busStopConfiguration;
        }

        private static List<TimePeriod> ValidTimePeriods
        {
            get
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
        }

        private static List<TimePeriod> EmptyTimePeriods => new();

        private static List<TimePeriod> CrossTimePeriods
        {
            get
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
                        From = DateTime.Parse("16:00:00"),
                        To = DateTime.Parse("19:59:59"),
                        RequestsCountCoefficient = 0.35f
                    },
                    new TimePeriod
                    {
                        From = DateTime.Parse("18:00:00"),
                        To = DateTime.Parse("23:59:59"),
                        RequestsCountCoefficient = 0.1f
                    }
                };

                return timePeriods;
            }
        }

        private static List<TimePeriod> TimePeriodsWhichDontCoverDaily24hInterval
        {
            get
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
                };

                return timePeriods;
            }
        }

        private static List<TimePeriod> TimePeriodsWithSummaryRequestsCountCoefficientMoreThan1
        {
            get
            {
                var timePeriods = new List<TimePeriod>
                {
                    new TimePeriod
                    {
                        From = DateTime.Parse("00:00:00"),
                        To = DateTime.Parse("06:59:59"),
                        RequestsCountCoefficient = 0.07f
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
                };

                return timePeriods;
            }
        }
    }
}
