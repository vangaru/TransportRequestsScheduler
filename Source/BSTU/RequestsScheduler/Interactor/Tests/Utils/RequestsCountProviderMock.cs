namespace BSTU.RequestsScheduler.Interactor.Tests.Utils
{
    internal static class RequestsCountProviderMock
    {
        private static readonly Random _random = new();

        public static int RequestsCount => _random.Next(ConfigurationMock.MinDailyRequestsCount, ConfigurationMock.MaxDailyRequestsCount);
    }
}
