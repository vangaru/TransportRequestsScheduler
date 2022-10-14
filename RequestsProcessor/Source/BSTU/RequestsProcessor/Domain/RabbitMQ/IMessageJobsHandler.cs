namespace BSTU.RequestsProcessor.Domain.RabbitMQ
{
    public interface IMessageJobsHandler : IDisposable
    {
        public void Setup();
        public void Subscribe();
        public void Stop();
    }
}
