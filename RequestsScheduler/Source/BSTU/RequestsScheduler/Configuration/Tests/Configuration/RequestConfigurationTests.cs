using BSTU.RequestsScheduler.Configuration.Configuration;
using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Configuration.Tests.Configuration.TestCases;
using BSTU.RequestsScheduler.Configuration.Validators;
using BSTU.RequestsScheduler.Interactor.Configuration;
using Moq;

namespace BSTU.RequestsScheduler.Configuration.Tests.Configuration
{
    public partial class RequestConfigurationTests
    {
        private readonly Mock<IRequestConfigurationValidator> _validator;
        private RequestConfiguration? _requestConfiguration;

        public RequestConfigurationTests()
        {
            var validationSucceeded = true;
            var validationFailed = false;

            _validator = new Mock<IRequestConfigurationValidator>();
            _validator
                .Setup(validator => validator
                    .Validate(It.Is<IEnumerable<BusStopConfiguration>>(config => config.Count() > 0),
                        out validationSucceeded))
                .Returns<RequestValidationException?>(null);
            _validator
                .Setup(validator => validator
                    .Validate(It.Is<IEnumerable<BusStopConfiguration>>(config => config.Count() == 0),
                        out validationFailed))
                .Returns(new RequestValidationException());
        }

        [Theory]
        [InlineData(@"..\..\..\Configuration\TestCases\ValidConfiguration.json")]
        public void Configuration_ConfigurationFileExistsAndValid_ReturnsValidConfiguration(
            string configurationFilePath)
        {
            _requestConfiguration = new RequestConfiguration(configurationFilePath, _validator.Object);
            AssertConfigurationEqual(ValidConfiguration.Configuration, _requestConfiguration.Configuration);
        }

        [Theory]
        [InlineData(@"..\..\..\Configuration\TestCases\EmptyConfiguration.json")]
        public void Configuration_ValidatorReturnsException_ExceptionThrown(
            string configurationFilePath)
        {
            _requestConfiguration = new RequestConfiguration(configurationFilePath, _validator.Object);
            Assert.Throws<RequestValidationException>(() => _requestConfiguration.Configuration);
        }

        [Theory]
        [InlineData(@"..\..\..\Configuration\TestCases\FileNotFound.json")]
        public void Configuration_ConfigurationFileNotExists_ExceptionThrown(
            string configurationFilePath)
        {
            _requestConfiguration = new RequestConfiguration(configurationFilePath, _validator.Object);
            Assert.Throws<FileNotFoundException>(() => _requestConfiguration.Configuration);
        }

        [Theory]
        [InlineData(@"..\..\..\Configuration\TestCases\NotSupportedExtension.txt")]
        public void Configuration_NotSupportedExtension_ExceptionThrown(
            string configurationFilePath)
        {
            _requestConfiguration = new RequestConfiguration(configurationFilePath, _validator.Object);
            Assert.Throws<NotSupportedException>(() => _requestConfiguration.Configuration);
        }
    }
}