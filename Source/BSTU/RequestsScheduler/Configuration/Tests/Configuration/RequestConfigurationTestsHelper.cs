using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Tests.Configuration
{
    public partial class RequestConfigurationTests
    {
        private void AssertConfigurationEqual(IEnumerable<BusStopConfiguration> expected,
            IEnumerable<BusStopConfiguration> actual)
        {
            Assert.Equal(expected.Count(), actual.Count());
            
            for (var i = 0; i < expected.Count(); i++)
            {
                AssertBusStopConfigurationEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        private void AssertBusStopConfigurationEqual(BusStopConfiguration expected, 
            BusStopConfiguration actual)
        {
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.DailyRequestsCount, actual.DailyRequestsCount);
            Assert.Equal(expected.TimePeriods.Count, actual.TimePeriods.Count);
            
            for (var i = 0; i < expected.TimePeriods.Count; i++)
            {
                AssertTimePeriodsEqual(expected.TimePeriods[i], actual.TimePeriods[i]);
            }
        }

        private void AssertTimePeriodsEqual(TimePeriod expected, TimePeriod actual)
        {
            Assert.Equal(expected.From, actual.From);
            Assert.Equal(expected.To, actual.To);
            Assert.Equal(expected.RequestsCountCoefficient, actual.RequestsCountCoefficient);
        }
    }
}