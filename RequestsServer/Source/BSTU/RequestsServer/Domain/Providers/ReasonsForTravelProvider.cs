using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BSTU.RequestsServer.Domain.Providers
{
    public class ReasonsForTravelProvider : ConfigRecordsProvider, IReasonsForTravelProvider
    {
        private const string ReasonsForTravelConfigPathKey = "ReasonsForTravelConfigPath";

        public ReasonsForTravelProvider(IConfiguration configuration) : base(configuration, ReasonsForTravelConfigPathKey)
        {
        }

        public List<string> ReasonsForTravel => Records;

        protected override List<string> ReadRecrodsFromConfig()
        {
            return JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(ConfigRecordsPath))
                ?? throw new ApplicationException($"Failed to deserialize {ConfigRecordsPath}");
        }
    }
}