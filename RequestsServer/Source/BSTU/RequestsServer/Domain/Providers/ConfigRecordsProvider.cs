using Microsoft.Extensions.Configuration;

namespace BSTU.RequestsServer.Domain.Providers
{
    public abstract class ConfigRecordsProvider
    {
        private List<string>? _records;

        public ConfigRecordsProvider(IConfiguration configuration, string configRecordsPathKey)
        {
            ConfigRecordsPath = configuration[configRecordsPathKey] 
                ?? throw new ApplicationException($"Key {configRecordsPathKey} is not defined.");
        }

        protected List<string> Records
        {
            get
            {
                if (_records == null || !_records.Any())
                {
                    _records = ReadRecrodsFromConfig();
                }

                return _records;
            }
        }

        protected string ConfigRecordsPath { get; }

        protected abstract List<string> ReadRecrodsFromConfig();
    }
}