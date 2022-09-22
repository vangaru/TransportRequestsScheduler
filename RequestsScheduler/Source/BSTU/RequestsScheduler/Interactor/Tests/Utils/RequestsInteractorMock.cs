using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Interactor.Tests.Utils
{
    internal static class RequestsInteractorMock
    {
        public static IEnumerable<Request> Requests
        {
            get
            {
                var requests = new List<Request>();

                foreach (string busStopName in ConfigurationMock.BusStopNames)
                {
                    Request request = RequestFactoryMock.CreateRequest(busStopName);
                    requests.Add(request);
                }

                return requests;
            }
        }
    }
}