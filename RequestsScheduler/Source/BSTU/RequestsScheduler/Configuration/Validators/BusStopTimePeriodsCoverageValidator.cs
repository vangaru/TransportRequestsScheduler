using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public class BusStopTimePeriodsCoverageValidator : IRequestConfigurationValidator
    {
        public RequestValidationException? Validate(IEnumerable<BusStopConfiguration> configuration, out bool success)
        {
            success = true;

            foreach (BusStopConfiguration busStop in configuration)
            {
                long totalTicks = (busStop.TimePeriods.Count * TimeSpan.TicksPerSecond) + busStop.TimePeriods
                    .Sum(period => period.To.TimeOfDay.Ticks - period.From.TimeOfDay.Ticks);

                success = TimeSpan.TicksPerDay == totalTicks;
                
                if (!success)
                {
                    return new RequestValidationException(
                        ValidationMessage.GetTimePeriodsDoNotCover24hIntervalMessage(busStop.Name));
                }
            }

            return null;
        }
    }
}