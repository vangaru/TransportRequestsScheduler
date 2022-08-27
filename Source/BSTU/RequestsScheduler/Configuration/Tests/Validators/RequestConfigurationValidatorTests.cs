using System.Text;
using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Configuration.Tests.Utils;
using BSTU.RequestsScheduler.Configuration.Validators;
using BSTU.RequestsScheduler.Interactor.Configuration;
using Moq;

namespace BSTU.RequestsScheduler.Configuration.Tests.Validators
{
    public class RequestConfigurationValidatorTests
    {
        private const string ValidationFailedMessage = "Validation Failed Message";

        private readonly RequestConfigurationValidator _validator;

        private readonly Mock<IRequestConfigurationValidator> _configurationValidator;        

        public RequestConfigurationValidatorTests()
        {
            var validationFailed = false;
            var validationSucceeded = true;

            _configurationValidator = new Mock<IRequestConfigurationValidator>();
            _configurationValidator
                .Setup(validator => validator
                    .Validate(It.Is<IEnumerable<BusStopConfiguration>>(config => config.Count() == 0),
                        out validationFailed))
                .Returns(new RequestValidationException(ValidationFailedMessage));
            _configurationValidator
                .Setup(validator => validator
                    .Validate(It.Is<IEnumerable<BusStopConfiguration>>(config => config.Count() > 0),
                        out validationSucceeded))
                .Returns<RequestValidationException?>(null);

            var validators = new List<IRequestConfigurationValidator>
            {
                _configurationValidator.Object, 
                _configurationValidator.Object, 
                _configurationValidator.Object
            };

            _validator = new RequestConfigurationValidator(validators);
        }

        [Fact]
        public void Validate_ValidationFailed_OutFalseReturnsValidationException()
        {
            RequestValidationException? exception = _validator.Validate(
                ConfigurationMock.EmptyConfiguration, out bool success);

            Assert.NotNull(exception);
            Assert.False(success);
            var builder = new StringBuilder();
            for (var i = 0; i < 3; i++)
            {
                builder.AppendLine(ValidationFailedMessage);
            }

            Assert.True(builder.ToString().Equals(exception!.Message.ToString(), 
                StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void Validate_ValidationSucceeded_OutTrueReturnsNull()
        {
            RequestValidationException? exception = _validator.Validate(
                ConfigurationMock.ValidConfiguration, out bool success);

            Assert.Null(exception);
            Assert.True(success);
        }
    }
}