namespace BSTU.RequestsScheduler.Interactor.Configuration
{
    public class TimePeriod
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public float RequestsCountCoefficient { get; set; }
    }
}
