using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Configuration.Validators;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Configuration
{
    public abstract class RequestConfigurationBase : IRequestConfigurationProxy
    {
        public RequestConfigurationBase(IRequestConfigurationValidator validator)
        {
            Validator = validator;
        }

        public IEnumerable<BusStopConfiguration> Configuration
        {
            get
            {
                IEnumerable<BusStopConfiguration> busStopConfiguration = RetrieveConfiguration();
                RequestValidationException? exception = Validator.Validate(busStopConfiguration, out bool result);
                return result 
                    ? busStopConfiguration 
                    : exception != null 
                        ? throw exception 
                        : throw new ApplicationException("Request Configuration failed due to unrecognized error");
            }
        }

        protected IRequestConfigurationValidator Validator { get; }

        protected abstract IEnumerable<BusStopConfiguration> RetrieveConfiguration();
    }
}