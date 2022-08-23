using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public class BusStopNamesValidator : IRequestConfigurationValidator
    {
        public RequestValidationException? Validate(IEnumerable<BusStopConfiguration> configuration, 
            out bool success)
        {
            IEnumerable<string> busStopNames = configuration.Select(busStop => busStop.Name);
            var distinctBusStopNames = new HashSet<string>(busStopNames);
            int duplicatesCount = distinctBusStopNames.Count - busStopNames.Count();
            success = duplicatesCount == 0;

            return success
                ? null
                : new RequestValidationException(
                    ValidationMessage.GetBusStopNamesNotUniqueMessage(duplicatesCount));
        }
    }
}