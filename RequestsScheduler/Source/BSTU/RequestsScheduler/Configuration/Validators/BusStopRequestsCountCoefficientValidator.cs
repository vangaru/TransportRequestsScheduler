using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public class BusStopRequestsCountCoefficientValidator : IRequestConfigurationValidator
    {
        private const float RequiredRequestsCountCoefficientSum = 1f;

        public RequestValidationException? Validate(IEnumerable<BusStopConfiguration> configuration, 
            out bool success)
        {
            success = true;

            foreach (var busStop in configuration)
            {
                float coefficientsCum = busStop.TimePeriods
                    .Sum(timePeriod => timePeriod.RequestsCountCoefficient);
                
                if (coefficientsCum != RequiredRequestsCountCoefficientSum)
                {
                    success = false;
                    return new RequestValidationException(
                        ValidationMessage.GetBusStopRequestsCountCoefficientNotEqualOneMessage(coefficientsCum, busStop.Name));
                }
            }

            return null;
        }
    }
}