using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Configuration.Tests.Utils;
using BSTU.RequestsScheduler.Configuration.Validators;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Tests.Validators
{
    public class BusStopCrossTimePeriodsValidatorTests
    {
        private readonly BusStopCrossTimePeriodsValidator _validator = new();

        public static IEnumerable<object[]> ValidInputData => new List<object[]>
        {
            new[] { ConfigurationMock.ValidConfiguration },
            new[] { ConfigurationMock.ConfigurationWithDailyRequestsCountLessThan1 },
            new[] { ConfigurationMock.ConfigurationWithRepeatedNames },
            new[] { ConfigurationMock.EmptyConfiguration },
            new[] { ConfigurationMock.ConfigurationWithTimePeriodsWhichDontCoverDaily24hInterval },
            new[] { ConfigurationMock.ConfigurationWithEmptyTimePeriods }
        };

        public static IEnumerable<object[]> InvalidInputData => new List<object[]>
        {
            new[] { ConfigurationMock.ConfigurationWithCrossTimePeriods }
        };

        [Theory]
        [MemberData(nameof(ValidInputData))]
        public void Validate_ValidTimePeriods_ReturnsTrueWithoutExceptions(IEnumerable<BusStopConfiguration> configuration)
        {
            RequestValidationException? validationException = _validator.Validate(configuration, out bool success);
            Assert.Null(validationException);
            Assert.True(success);
        }

        [Theory]
        [MemberData(nameof(InvalidInputData))]
        public void Validate_InvalidTimePeriods_ReturnsFalseWithExceptions(IEnumerable<BusStopConfiguration> configuration)
        {
            RequestValidationException? validationException = _validator.Validate(configuration, out bool success);
            Assert.NotNull(validationException);
            Assert.False(success);
        }
    }
}
