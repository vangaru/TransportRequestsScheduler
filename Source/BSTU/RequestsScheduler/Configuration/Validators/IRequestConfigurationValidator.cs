using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public interface IRequestConfigurationValidator
    {
        public RequestValidationException? Validate(IEnumerable<BusStopConfiguration> confiugration, out bool result);
    }
}