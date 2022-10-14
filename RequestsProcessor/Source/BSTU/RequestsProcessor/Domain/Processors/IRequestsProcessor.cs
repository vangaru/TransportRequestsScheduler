namespace BSTU.RequestsProcessor.Domain.Processors
{
    public interface IRequestsProcessor
    {
        public Task ProcessAsync(string requestContent);
    }
}
