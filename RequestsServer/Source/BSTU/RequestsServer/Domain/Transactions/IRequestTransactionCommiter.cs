using BSTU.RequestsServer.Domain.Models;

namespace BSTU.RequestsServer.Domain.Transactions
{
    public interface IRequestTransactionCommiter
    {
        public Task CommitTransactionAsync(Request request);
    }
}
