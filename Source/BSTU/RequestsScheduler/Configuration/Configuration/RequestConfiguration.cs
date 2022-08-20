using BSTU.RequestsScheduler.Configuration.Validators;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Configuration
{
    public class RequestConfiguration : IRequestConfigurationProxy
    {
        private readonly string _configurationFilePath;
        private readonly IRequestConfigurationValidator _validator;

        public RequestConfiguration(string configurationFilePath, IRequestConfigurationValidator validator)
        {
            _configurationFilePath = configurationFilePath;
            _validator = validator;
        }

        public IEnumerable<BusStopConfiguration> Configuration => throw new NotImplementedException();
    }
}