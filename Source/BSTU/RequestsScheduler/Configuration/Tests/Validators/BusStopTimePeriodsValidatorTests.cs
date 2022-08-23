using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Configuration.Tests.Utils;
using BSTU.RequestsScheduler.Configuration.Validators;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Tests.Validators
{
    public class BusStopTimePeriodsValidatorTests
    {
        private readonly BusStopTimePeriodsValidator _validator = new();

        public static IEnumerable<object[]> ValidInputData => new List<object[]>
        {
            new[] { ConfigurationMock.ValidConfiguration },
            new[] { ConfigurationMock.ConfigurationWithDailyRequestsCountLessThan1 },
            new[] { ConfigurationMock.ConfigurationWithRepeatedNames }
        };

        public static IEnumerable<object[]> InvalidInputData => new List<object[]>
        {
            new[] { ConfigurationMock.EmptyConfiguration },
            new[] { ConfigurationMock.ConfigurationWithCrossTimePeriods },
            new[] { ConfigurationMock.ConfigurationWithEmptyTimePeriods },
            new[] { ConfigurationMock.ConfigurationWithTimePeriodsWhichDontCoverDaily24hInterval },
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
        [MemberData(nameof(ValidInputData))]
        public void Validate_InvalidTimePeriods_ReturnsFalseWithExceptions(IEnumerable<BusStopConfiguration> configuration)
        {
            RequestValidationException? validationException = _validator.Validate(configuration, out bool success);
            Assert.NotNull(validationException);
            Assert.False(success);
        }
    }
}
