using Microsoft.Extensions.Configuration;

namespace BSTU.RequestsProcessor.Domain.Processors
{
    public class FaultedRequestsCountLogger : IFaultedRequestsCountLogger
    {
        private const string FaultedRequestsFilePathConfigurationKey = "FaultedRequestsFilePath";
        private readonly string _path;

        public FaultedRequestsCountLogger(IConfiguration configuration)
        {
            _path = configuration[FaultedRequestsFilePathConfigurationKey];
        }

        public async Task IncrementFaultedRequestsCountAsync()
        {
            string fileContents = await File.ReadAllTextAsync(_path);
            if (string.IsNullOrWhiteSpace(fileContents))
            {
                await File.WriteAllTextAsync(_path, 1.ToString());
            }
            int currentCount = int.Parse(fileContents);
            await File.WriteAllTextAsync(_path, (++currentCount).ToString());
        }
    }
}