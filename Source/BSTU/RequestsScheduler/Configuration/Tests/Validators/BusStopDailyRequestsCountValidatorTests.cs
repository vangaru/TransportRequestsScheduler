using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Configuration.Tests.Utils;
using BSTU.RequestsScheduler.Configuration.Validators;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Tests.Validators
{
    public class BusStopDailyRequestsCountValidatorTests
    {
        private readonly BusStopDailyRequestsCountValidator _validator = new();

        public static IEnumerable<object[]> ValidInputData => new List<object[]>
        {
            new[] { ConfigurationMock.ValidConfiguration },
            new[] { ConfigurationMock.ConfigurationWithRepeatedNames },
            new[] { ConfigurationMock.ConfigurationWithSummaryRequestsCoefficientMoreThan1 },
            new[] { ConfigurationMock.ConfigurationWithCrossTimePeriods },
            new[] { ConfigurationMock.ConfigurationWithEmptyTimePeriods },
            new[] { ConfigurationMock.ConfigurationWithTimePeriodsWhichDontCoverDaily24hInterval },
            new[] { ConfigurationMock.EmptyConfiguration },
        };

        public static IEnumerable<object[]> InvalidInputData => new List<object[]>
        {
            new[] { ConfigurationMock.ConfigurationWithDailyRequestsCountLessThan1 }
        };

        [Theory]
        [MemberData(nameof(ValidInputData))]
        public void Validate_DailyRequestsCountGreaterOrEqualOne_ReturnsTrueWithoutException(IEnumerable<BusStopConfiguration> configuration)
        {
            RequestValidationException? validationException = _validator.Validate(configuration, out bool success);
            Assert.Null(validationException);
            Assert.True(success);
        }

        [Theory]
        [MemberData(nameof(InvalidInputData))]
        public void Validate_DailyRequestsCountLessThanOne_ReturnsFalseWithException(IEnumerable<BusStopConfiguration> configuration)
        {
            RequestValidationException? validationException = _validator.Validate(configuration, out bool success);
            Assert.NotNull(validationException);
            Assert.False(success);
        }
    }
}