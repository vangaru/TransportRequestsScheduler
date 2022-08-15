using BSTU.RequestsScheduler.Interactor.Configuration;
using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Interactor.Factories
{
    public class RequestFactory : IRequestFactory
    {
        public RequestFactory(IRequestConfigurationProxy configuration)
        {
        }

        public Request Create(string busStopName)
        {
            throw new NotImplementedException();
        }
    }
}