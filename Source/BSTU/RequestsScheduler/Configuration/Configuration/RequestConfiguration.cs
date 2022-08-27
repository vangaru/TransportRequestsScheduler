using BSTU.RequestsScheduler.Configuration.Validators;
using BSTU.RequestsScheduler.Interactor.Configuration;
using Newtonsoft.Json;

namespace BSTU.RequestsScheduler.Configuration.Configuration
{
    public class RequestConfiguration : RequestConfigurationBase
    {
        private const string JsonExtension = ".json";

        private readonly string _configurationFilePath;

        public RequestConfiguration(string configurationFilePath, IRequestConfigurationValidator validator) : base(validator)
        {
            _configurationFilePath = configurationFilePath;
        }

        protected override IEnumerable<BusStopConfiguration> RetrieveConfiguration()
        {
            if (IsConfigExtensionSupported())
            {
                throw new NotSupportedException($"{Path.GetExtension(_configurationFilePath)} extension is not supported.");
            }

            string configContents = File.ReadAllText(_configurationFilePath);
            return JsonConvert.DeserializeObject<List<BusStopConfiguration>>(configContents);
        }

        private bool IsConfigExtensionSupported() => string.IsNullOrEmpty(Path.GetExtension(_configurationFilePath))
            || !JsonExtension.Equals(Path.GetExtension(_configurationFilePath), StringComparison.InvariantCultureIgnoreCase);
    }
}