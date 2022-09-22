using BSTU.RequestsScheduler.Interactor.Configuration;
using BSTU.RequestsScheduler.Interactor.Providers;
using BSTU.RequestsScheduler.Interactor.Tests.TestData;
using BSTU.RequestsScheduler.Interactor.Tests.Utils;
using Moq;

namespace BSTU.RequestsScheduler.Interactor.Tests.Providers
{
    public class RequestsCountProviderTests
    {
        private readonly Mock<IRequestConfigurationProxy> _configuration;
        private readonly IRequestsCountProvider _requestsCountProvider;

        public RequestsCountProviderTests()
        {
            _configuration = new Mock<IRequestConfigurationProxy>();
            _configuration.Setup(c => c.Configuration).Returns(ConfigurationMock.Configuration);
            _requestsCountProvider = new RequestsCountProvider(_configuration.Object);
        }

        [Theory]
        [ClassData(typeof(BusStopNamesTestData))]
        public void GetRequestsCount_ValidConfiguraiton_ValidRequestsCount(string busStopName)
        {
            int actualRequestsCount = _requestsCountProvider.GetRequestsCountForCurrentPeriod(busStopName);
            BusStopConfiguration busStop = _configuration.Object.Configuration
                .First(busStop => string.Compare(busStop.Name, busStopName) == 0);
            TimePeriod currentPeriod = busStop.TimePeriods
                .First(period => period.From.TimeOfDay <= DateTime.Now.TimeOfDay && period.To.TimeOfDay >= DateTime.Now.TimeOfDay);
             var expectedRequestsCount = Convert.ToInt32(Math.Ceiling(busStop.DailyRequestsCount * currentPeriod.RequestsCountCoefficient));
            Assert.Equal(expectedRequestsCount, actualRequestsCount);
        }
    }
}