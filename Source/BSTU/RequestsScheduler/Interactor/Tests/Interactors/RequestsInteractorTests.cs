using BSTU.RequestsScheduler.Interactor.Configuration;
using BSTU.RequestsScheduler.Interactor.Factories;
using BSTU.RequestsScheduler.Interactor.Interactors;
using BSTU.RequestsScheduler.Interactor.Models;
using BSTU.RequestsScheduler.Interactor.Providers;
using BSTU.RequestsScheduler.Interactor.Tests.Utils;
using Moq;

namespace BSTU.RequestsScheduler.Interactor.Tests.Interactors
{
    public class RequestsInteractorTests
    {
        private readonly Mock<IRequestsCountProvider> _requestsCountProvider;
        private readonly Mock<IRequestFactory> _requestFactory;
        private readonly Mock<IRequestConfigurationProxy> _configuration;
        private readonly IRequestsInteractor _requestsInteractor;

        public RequestsInteractorTests()
        {
            _configuration = new Mock<IRequestConfigurationProxy>();
            _configuration
                .Setup(config => config.Configuration)
                .Returns(ConfigurationMock.Configuration);

            _requestsCountProvider = new Mock<IRequestsCountProvider>();
            _requestsCountProvider
                .Setup(provider => provider.GetRequestsCountForCurrentPeriod(It.IsAny<string>()))
                .Returns(RequestsCountProviderMock.RequestsCount);

            _requestFactory = new Mock<IRequestFactory>();
            _requestFactory
                .Setup(factory => factory.Create(It.IsAny<string>()))
                .Returns((string busStopName) => RequestFactoryMock.CreateRequest(busStopName));
            _requestsInteractor = new RequestsInteractor(_configuration.Object, _requestsCountProvider.Object, _requestFactory.Object);
        }

        [Fact]
        public void Requests_ValidConfiguration_RequestsCountValid()
        {
            IEnumerable<Request> requests = _requestsInteractor.Requests;
            int expectedCountOfRequests = _configuration.Object.Configuration
                .Sum(busStop => _requestsCountProvider.Object.GetRequestsCountForCurrentPeriod(busStop.Name));
            Assert.Equal(expectedCountOfRequests, requests.Count());
        }

        [Fact]
        public void Requests_ValidConfiguration_RequestsCountForSpecificBusStopsValid()
        {
            IEnumerable<Request> requests = _requestsInteractor.Requests;
            foreach (BusStopConfiguration busStop in _configuration.Object.Configuration)
            {
                int expectedCount = _requestsCountProvider.Object.GetRequestsCountForCurrentPeriod(busStop.Name);
                int actualCount = requests
                    .Where(request => request.SourceBusStopName.Equals(busStop.Name, StringComparison.InvariantCultureIgnoreCase)).Count();
                Assert.Equal(expectedCount, actualCount);
            }
        }
    }
}