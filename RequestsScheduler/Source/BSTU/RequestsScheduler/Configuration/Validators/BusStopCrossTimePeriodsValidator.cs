using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public class BusStopCrossTimePeriodsValidator : IRequestConfigurationValidator
    {
        public RequestValidationException? Validate(IEnumerable<BusStopConfiguration> configuration, out bool success)
        {
            foreach (BusStopConfiguration busStop in configuration)
            {
                foreach (TimePeriod timePeriod in busStop.TimePeriods)
                {
                    if (busStop.TimePeriods.Any(period => TimePeriodIsCrossed(timePeriod, period)))
                    {
                        success = false;
                        return new RequestValidationException(ValidationMessage.GetTimePeriodsCrossedMessage(busStop.Name));
                    }
                }
            }

            success = true;
            return null;
        }

        private bool TimePeriodIsCrossed(TimePeriod crossedTimePeriod, TimePeriod crossingTimePeriod)
        {
            return crossedTimePeriod != crossingTimePeriod
                && (CrossedFromBound(crossedTimePeriod, crossingTimePeriod) 
                || CrossedToBound(crossedTimePeriod, crossingTimePeriod));
        }

        private bool CrossedFromBound(TimePeriod crossedPeriod, TimePeriod crossingPeriod)
        {
            return crossedPeriod.From.TimeOfDay >= crossingPeriod.From.TimeOfDay
                && crossedPeriod.From.TimeOfDay <= crossingPeriod.To.TimeOfDay;
        }

        private bool CrossedToBound(TimePeriod crossedPeriod, TimePeriod crossingPeriod)
        {
            return crossedPeriod.To.TimeOfDay >= crossingPeriod.From.TimeOfDay
                && crossedPeriod.To.TimeOfDay <= crossingPeriod.To.TimeOfDay;
        }
    }
}