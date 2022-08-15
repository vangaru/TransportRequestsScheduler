using BSTU.RequestsScheduler.Interactor.Configuration;
using BSTU.RequestsScheduler.Interactor.Factories;
using BSTU.RequestsScheduler.Interactor.Models;
using BSTU.RequestsScheduler.Interactor.Tests.Factories.TestData;
using BSTU.RequestsScheduler.Interactor.Tests.Utils;
using Moq;

namespace BSTU.RequestsScheduler.Interactor.Tests.Factories
{
    public class RequestFactoryTests
    {
        private const int MinSeatsCount = 1;
        private const int MaxSeatsCount = 3;

        private readonly Mock<IRequestConfigurationProxy> _configuration;
        private readonly IRequestFactory _requestFactory;
        
        public RequestFactoryTests()
        {
            _configuration = new Mock<IRequestConfigurationProxy>();
            _configuration.Setup(c => c.Configuration).Returns(ConfigurationMock.Configuration);
            _requestFactory = new RequestFactory(_configuration.Object);
        }

        [Theory]
        [ClassData(typeof(BusStopNamesTestData))]
        public void CreateRequest_ValidConfiguration_RequestCreatedSuccessfully(string sourceBusStopName)
        {
            Request request = _requestFactory.Create(sourceBusStopName);
            Assert.Equal(sourceBusStopName, request.SourceBusStopName);
            Assert.NotEqual(sourceBusStopName, request.DestinationBusStopName);
            Assert.Contains(request.SourceBusStopName, _configuration.Object.Configuration.Select(busStop => busStop.Name));
            Assert.Contains(request.DestinationBusStopName, _configuration.Object.Configuration.Select(busStop => busStop.Name));
            Assert.InRange(request.SeatsCount, MinSeatsCount, MaxSeatsCount);
            TimePeriod currentTimePeriod = _configuration.Object.Configuration.First(busStop => string.Compare(request.SourceBusStopName, busStop.Name) == 0)
                .TimePeriods.First(period => period.From.TimeOfDay <= DateTime.Now.TimeOfDay && period.To.TimeOfDay >= DateTime.Now.TimeOfDay);
            Assert.InRange(request.DateTime.Hour, currentTimePeriod.From.Hour, currentTimePeriod.To.Hour);
        }
    }
}