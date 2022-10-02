using BSTU.RequestsServer.Api.Models;
using BSTU.RequestsServer.Domain.Transactions;

namespace BSTU.RequestsServer.Api.Handlers
{
    public class RequestsHandler : IRequestsHandler
    {
        private readonly ILogger<RequestsHandler> _logger;
        private readonly IRequestTransactionCommiter _transactionCommiter;

        public RequestsHandler(ILogger<RequestsHandler> logger, IRequestTransactionCommiter transactionCommiter)
        {
            _logger = logger;
            _transactionCommiter = transactionCommiter;
        }

        public async Task HandleRequest(Request request)
        {

            await _transactionCommiter.CommitTransactionAsync(request);
            _logger.LogInformation(request.ToString());
        }
    }
}