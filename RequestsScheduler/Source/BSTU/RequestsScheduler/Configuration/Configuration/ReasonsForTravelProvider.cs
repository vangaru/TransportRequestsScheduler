using BSTU.RequestsScheduler.Interactor.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BSTU.RequestsScheduler.Configuration.Configuration
{
    public class ReasonsForTravelProvider : IReasonsForTravelProxy
    {
        private const string ReasonsForTravelConfigPathKey = "ReasonsForTravelConfigPath";

        private readonly string _reasonsForTravelConfigPath;

        private List<string>? _reasonsForTravel;

        public ReasonsForTravelProvider(IConfiguration configuration)
        {
            _reasonsForTravelConfigPath = configuration[ReasonsForTravelConfigPathKey];
        }

        public List<string> ReasonsForTravel
        {
            get
            {
                if (_reasonsForTravel == null || !_reasonsForTravel.Any())
                {
                    _reasonsForTravel = ReadReasonsForTravelFromConfig();
                }

                return _reasonsForTravel;
            }
        }

        private List<string> ReadReasonsForTravelFromConfig()
        {
            return JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(_reasonsForTravelConfigPath));
        }
    }
}