using BSTU.RequestsProcessor.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BSTU.RequestsProcessor.Domain.PostgreSQL
{
    public sealed class RequestsProcessorContext : DbContext
    {
        public DbSet<Request> Requests => Set<Request>();

        public RequestsProcessorContext(DbContextOptions<RequestsProcessorContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}