using System.Text;

namespace BSTU.RequestsScheduler.Worker.Loggers
{
    public class StatisticsLogger : IStatisticsLogger
    {
        private const string StatisticsLogsPathKey = "StatisticsLogger.Path";

        private readonly string _statisticsLogsPath;

        public StatisticsLogger(IConfiguration configuration)
        {
            _statisticsLogsPath = configuration[StatisticsLogsPathKey];
        }

        public async Task LogAsync(TimeSpan from, TimeSpan to, string busStopName, 
            int expectedRequestsCount, int actualSubmittedRequestsCount)
        {
            string statistics = GetStatistics(from, to, busStopName, expectedRequestsCount, actualSubmittedRequestsCount);
            using var logsWriter = new StreamWriter(_statisticsLogsPath, append: true);
            await logsWriter.WriteLineAsync(statistics);
        }

        private string GetStatistics(TimeSpan from, TimeSpan to, string busStopName,
            int expectedRequestsCount, int actualSubmittedRequestsCount)
        {
            return String.Empty;
            var statisticsBuilder = new StringBuilder();
            statisticsBuilder
                .AppendLine($"Statistics for {busStopName}")
                .Append($"for time interval from {from.ToString()} to {to.ToString()}")
                .AppendLine($"Expected number of requests: {expectedRequestsCount}")
                .AppendLine($"Actual number of requests: {actualSubmittedRequestsCount}");

            return statisticsBuilder.ToString();
        }
    }
}