using BSTU.RequestsServer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BSTU.RequestsServer.Domain.PostgreSQL
{
    public sealed class RequestsContext : DbContext
    {
        public DbSet<Request> Requests => Set<Request>();

        public RequestsContext(DbContextOptions<RequestsContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}