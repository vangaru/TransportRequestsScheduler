using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public class BusStopTimePeriodBoundsValidator : IRequestConfigurationValidator
    {
        public RequestValidationException? Validate(IEnumerable<BusStopConfiguration> configuration, 
            out bool success)
        {
            success = true;

            foreach (BusStopConfiguration busStop in configuration)
            {
                foreach (TimePeriod timePeriod in busStop.TimePeriods)
                {
                    success = timePeriod.From.TimeOfDay < timePeriod.To.TimeOfDay;
                    if (!success)
                    {
                        return new RequestValidationException(ValidationMessage
                            .GetInvalidTimePeriodBoundsMessage(busStop.Name, timePeriod.From.TimeOfDay, timePeriod.To.TimeOfDay));
                    }
                }
            }

            return null;
        }
    }
}