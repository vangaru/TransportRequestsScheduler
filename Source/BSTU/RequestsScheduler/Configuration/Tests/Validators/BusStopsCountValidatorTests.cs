using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Configuration.Tests.Utils;
using BSTU.RequestsScheduler.Configuration.Validators;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Tests.Validators
{
    public class BusStopsCountValidatorTests
    {
        private readonly BusStopsCountValidator _validator = new();

        public static IEnumerable<object[]> ValidInputData => new List<object[]>
        {
            new[] { ConfigurationMock.ValidConfiguration },
            new[] { ConfigurationMock.ConfigurationWithCrossTimePeriods },
            new[] { ConfigurationMock.ConfigurationWithEmptyTimePeriods },
            new[] { ConfigurationMock.ConfigurationWithTimePeriodsWhichDontCoverDaily24hInterval },
            new[] { ConfigurationMock.ConfigurationWithDailyRequestsCountLessThan1 },
            new[] { ConfigurationMock.ConfigurationWithRepeatedNames }
        };

        public static IEnumerable<object[]> InvalidInputData => new List<object[]>
        {
            new[] { ConfigurationMock.EmptyConfiguration },
        };

        [Theory]
        [MemberData(nameof(ValidInputData))]
        public void Validate_BusStopsCountGreaterOrEqual2_ReturnsTrueWithoutExceptions(IEnumerable<BusStopConfiguration> configuration)
        {
            RequestValidationException? validationException = _validator.Validate(configuration, out bool success);
            Assert.Null(validationException);
            Assert.True(success);
        }

        [Theory]
        [MemberData(nameof(ValidInputData))]
        public void Validate_BusStopsCountLessThan2_ReturnsFalseWithExceptions(IEnumerable<BusStopConfiguration> configuration)
        {
            RequestValidationException? validationException = _validator.Validate(configuration, out bool success);
            Assert.NotNull(validationException);
            Assert.False(success);
        }
    }
}
