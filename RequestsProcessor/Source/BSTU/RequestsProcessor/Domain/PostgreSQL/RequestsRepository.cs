using BSTU.RequestsProcessor.Domain.Models;

namespace BSTU.RequestsProcessor.Domain.PostgreSQL
{
    public class RequestsRepository : IRequestsRepository
    {
        private readonly RequestsProcessorContext _context;

        public RequestsRepository(RequestsProcessorContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
        }
    }
}