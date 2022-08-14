using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Interactor.Factories
{
    public interface IRequestsFactory
    {
        public Request Create(string name);
    }
}