namespace BSTU.RequestsProcessor.Domain.Processors
{
    public interface IFaultedRequestsCountLogger
    {
        public Task IncrementFaultedRequestsCountAsync();
    }
}
