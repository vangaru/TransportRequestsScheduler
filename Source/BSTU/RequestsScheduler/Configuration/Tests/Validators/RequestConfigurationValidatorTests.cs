using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Configuration.Tests.Utils;
using BSTU.RequestsScheduler.Configuration.Validators;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Tests.Validators
{
    public class RequestConfigurationValidatorTests
    {
        private readonly RequestConfigurationValidator _validator = new();

        [Fact]
        public void Validate_CorrectData_ReturnsTrueWithoutExceptions()
        {
            RequestValidationException? exception = _validator.Validate(ConfigurationMock.ValidConfiguration, out bool success);
            Assert.Null(exception);
            Assert.True(success);
        }

        [Theory]
        [MemberData(nameof(ValidateMethodInputData))]
        public void Validate_InvalidData_ReturnsFalseWithException(IEnumerable<BusStopConfiguration> configuration)
        {
            RequestValidationException? exception = _validator.Validate(configuration, out bool success);
            Assert.NotNull(exception);
            Assert.False(success);
        }

        public static IEnumerable<object[]> ValidateMethodInputData => new List<object[]>
        {
            new[] { ConfigurationMock.EmptyConfiguration },
            new[] { ConfigurationMock.ConfigurationWithRepeatedNames },
            new[] { ConfigurationMock.ConfigurationWithSummaryRequestsCoefficientMoreThan1 },
            new[] { ConfigurationMock.ConfigurationWithDailyRequestsCountLessThan1 },
            new[] { ConfigurationMock.ConfigurationWithCrossTimePeriods },
            new[] { ConfigurationMock.ConfigurationWithEmptyTimePeriods },
            new[] { ConfigurationMock.ConfigurationWithTimePeriodsWhichDontCoverDaily24hInterval }
        };
    }
}