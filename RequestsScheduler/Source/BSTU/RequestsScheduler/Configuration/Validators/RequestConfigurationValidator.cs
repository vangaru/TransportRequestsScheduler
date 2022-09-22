using System.Text;
using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public class RequestConfigurationValidator : IRequestConfigurationValidator
    {
        private readonly IEnumerable<IRequestConfigurationValidator> _validators;

        public RequestConfigurationValidator(IEnumerable<IRequestConfigurationValidator> validators)
        {
            _validators = validators;
        }

        public RequestValidationException? Validate(IEnumerable<BusStopConfiguration> configuration, out bool success)
        {
            success = true;
            var summaryExceptionMessageBuilder = new StringBuilder();
            foreach (IRequestConfigurationValidator validator in _validators)
            {
                RequestValidationException? exception = validator.Validate(configuration, out bool validationSuccess);
                success &= validationSuccess;
                
                if (exception != null && !string.IsNullOrWhiteSpace(exception.Message))
                {
                    summaryExceptionMessageBuilder.AppendLine(exception.Message);
                }
            }

            return success 
                ? null 
                : new RequestValidationException(summaryExceptionMessageBuilder.ToString());
        }
    }
}