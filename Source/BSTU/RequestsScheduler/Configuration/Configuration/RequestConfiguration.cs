using BSTU.RequestsScheduler.Configuration.Validators;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Configuration
{
    public class RequestConfiguration : RequestConfigurationBase
    {
        private readonly string _configurationFilePath;

        public RequestConfiguration(string configurationFilePath, IRequestConfigurationValidator validator) : base(validator)
        {
            _configurationFilePath = configurationFilePath;
        }

        protected override IEnumerable<BusStopConfiguration> RetrieveConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}