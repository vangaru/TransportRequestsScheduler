using BSTU.RequestsProcessor.Domain.Models;

namespace BSTU.RequestsProcessor.Domain.PostgreSQL
{
    public interface IRequestsRepository
    {
        public Task AddAsync(Request request);
    }
}
