using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Tests.Configuration.TestCases
{
    internal static class ValidConfiguration
    {
        public static IEnumerable<BusStopConfiguration> Configuration => new List<BusStopConfiguration>
        {
            new BusStopConfiguration
            {
                Name = "Stop1",
                DailyRequestsCount = 1000,
                TimePeriods = new List<TimePeriod>
                {
                    new TimePeriod
                    {
                        From = DateTime.Parse("00:00:00"),
                        To = DateTime.Parse("06:59:59"),
                        RequestsCountCoefficient = 0.3f
                    },
                    new TimePeriod
                    {
                        From = DateTime.Parse("07:00:00"),
                        To = DateTime.Parse("18:59:59"),
                        RequestsCountCoefficient = 0.5f
                    },
                    new TimePeriod
                    {
                        From = DateTime.Parse("19:00:00"),
                        To = DateTime.Parse("23:59:59"),
                        RequestsCountCoefficient = 0.2f
                    }
                }
            },
            new BusStopConfiguration
            {
                Name = "Stop2",
                DailyRequestsCount = 1200,
                TimePeriods = new List<TimePeriod>
                {
                    new TimePeriod
                    {
                        From = DateTime.Parse("00:00:00"),
                        To = DateTime.Parse("11:59:59"),
                        RequestsCountCoefficient = 0.5f
                    },
                    new TimePeriod
                    {
                        From = DateTime.Parse("12:00:00"),
                        To = DateTime.Parse("23:59:59"),
                        RequestsCountCoefficient = 0.5f
                    }
                }
            },
            new BusStopConfiguration
            {
                Name = "Stop3",
                DailyRequestsCount = 1500,
                TimePeriods = new List<TimePeriod>
                {
                    new TimePeriod
                    {
                        From = DateTime.Parse("00:00:00"),
                        To = DateTime.Parse("05:59:59"),
                        RequestsCountCoefficient = 0.1f
                    },
                    new TimePeriod
                    {
                        From = DateTime.Parse("06:00:00"),
                        To = DateTime.Parse("10:30:00"),
                        RequestsCountCoefficient = 0.3f
                    },
                    new TimePeriod
                    {
                        From = DateTime.Parse("10:30:01"),
                        To = DateTime.Parse("15:59:59"),
                        RequestsCountCoefficient = 0.2f
                    },
                    new TimePeriod
                    {
                        From = DateTime.Parse("16:00:00"),
                        To = DateTime.Parse("21:00:00"),
                        RequestsCountCoefficient = 0.3f
                    },
                    new TimePeriod
                    {
                        From = DateTime.Parse("21:00:01"),
                        To = DateTime.Parse("23:59:59"),
                        RequestsCountCoefficient = 0.1f
                    }
                }
            },
            new BusStopConfiguration
            {
                Name = "Stop4",
                DailyRequestsCount = 900,
                TimePeriods = new List<TimePeriod>
                {
                    new TimePeriod
                    {
                        From = DateTime.Parse("00:00:00"),
                        To = DateTime.Parse("05:59:59"),
                        RequestsCountCoefficient = 0.1f
                    },
                    new TimePeriod
                    {
                        From = DateTime.Parse("06:00:00"),
                        To = DateTime.Parse("21:00:00"),
                        RequestsCountCoefficient = 0.8f
                    },
                    new TimePeriod
                    {
                        From = DateTime.Parse("21:00:01"),
                        To = DateTime.Parse("23:59:59"),
                        RequestsCountCoefficient = 0.1f
                    }
                }
            },
            new BusStopConfiguration
            {
                Name = "Stop5",
                DailyRequestsCount = 1500,
                TimePeriods = new List<TimePeriod>
                {
                    new TimePeriod
                    {
                        From = DateTime.Parse("00:00:00"),
                        To = DateTime.Parse("05:59:59"),
                        RequestsCountCoefficient = 0.1f
                    },
                    new TimePeriod
                    {
                        From = DateTime.Parse("06:00:00"),
                        To = DateTime.Parse("21:00:00"),
                        RequestsCountCoefficient = 0.8f
                    },
                    new TimePeriod
                    {
                        From = DateTime.Parse("21:00:01"),
                        To = DateTime.Parse("23:59:59"),
                        RequestsCountCoefficient = 0.1f
                    }
                }
            },
        };
    }
}