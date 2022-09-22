using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public class BusStopDailyRequestsCountValidator : IRequestConfigurationValidator
    {
        private const int MinDailyRequestsCount = 1;

        public RequestValidationException? Validate(IEnumerable<BusStopConfiguration> configuration, 
            out bool success)
        {
            success = true;

            foreach (BusStopConfiguration busStop in configuration)
            {
                if (busStop.DailyRequestsCount < MinDailyRequestsCount)
                {
                    success = false;
                    string validationMessage = ValidationMessage
                        .GetDailyRequestsCountLessThanOneMessage(busStop.DailyRequestsCount, busStop.Name);
                    return new RequestValidationException(validationMessage);
                }
            }

            return null;
        }
    }
}