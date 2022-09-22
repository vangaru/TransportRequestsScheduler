using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Interactor.Factories
{
    public interface IRequestFactory
    {
        public Request Create(string name);
    }
}