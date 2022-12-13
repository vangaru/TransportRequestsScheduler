using BSTU.RequestsServer.Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BSTU.RequestsServer.Domain.Providers
{
    public class BusStopNamesProvider : ConfigRecordsProvider, IBusStopNamesProvider
    {
        private const string RequestsConfigPathKey = "RequestsConfigPath";

        private List<string>? _busStopNames;

        public BusStopNamesProvider(IConfiguration configuration) : base(configuration, RequestsConfigPathKey)
        {
        }

        public List<string> BusStopNames => Records;

        protected override List<string> ReadRecrodsFromConfig()
        {
            List<dynamic> requests = JsonConvert.DeserializeObject<List<dynamic>>(File.ReadAllText(ConfigRecordsPath))
                        ?? throw new ApplicationException($"Failed to deserialize {ConfigRecordsPath}");
            _busStopNames = requests.Select(request => (string)request.Name).ToList();

            return _busStopNames;
        }
    }
}