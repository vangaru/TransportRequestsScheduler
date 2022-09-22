namespace BSTU.RequestsScheduler.Interactor.Configuration
{
    public class BusStopConfiguration
    {
        private string? _name;

        public string Name
        {
            get => _name ?? throw new ApplicationException("Name of the Bus Stop is not configured.");
            set => _name = value;
        }
        public int DailyRequestsCount { get; set; }
        public List<TimePeriod> TimePeriods { get; set; } = new();
    }
}