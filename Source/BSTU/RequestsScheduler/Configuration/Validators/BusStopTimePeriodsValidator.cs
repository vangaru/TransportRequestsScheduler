using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public class BusStopTimePeriodsValidator : IRequestConfigurationValidator
    {
        public RequestValidationException? Validate(IEnumerable<BusStopConfiguration> configuration, out bool success)
        {
            throw new NotImplementedException();
        }
    }
}
