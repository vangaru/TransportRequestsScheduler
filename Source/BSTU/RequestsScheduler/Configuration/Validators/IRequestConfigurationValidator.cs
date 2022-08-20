using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public interface IRequestConfigurationValidator
    {
        public bool Validate(IEnumerable<BusStopConfiguration> confiugration);
    }
}
