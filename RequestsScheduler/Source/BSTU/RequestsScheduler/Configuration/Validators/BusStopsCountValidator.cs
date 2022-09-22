using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public class BusStopsCountValidator : IRequestConfigurationValidator
    {
        private const int MinBusStopsCount = 2;

        public RequestValidationException? Validate(IEnumerable<BusStopConfiguration> configuration, 
            out bool success)
        {
            int busStopsCount = configuration.Count();
            success = busStopsCount >= MinBusStopsCount;

            return success
                ? null
                : new RequestValidationException(
                    ValidationMessage.GetBusStopsCountLessThanTwoMessage(busStopsCount));
        }
    }
}
