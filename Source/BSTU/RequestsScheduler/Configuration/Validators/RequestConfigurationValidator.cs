using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public class RequestConfigurationValidator : IRequestConfigurationValidator
    {
        public RequestValidationException? Validate(IEnumerable<BusStopConfiguration> confiugration, out bool result)
        {
            throw new NotImplementedException();
        }
    }
}