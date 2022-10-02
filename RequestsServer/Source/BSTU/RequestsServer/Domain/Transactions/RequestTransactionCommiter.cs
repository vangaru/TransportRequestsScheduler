using BSTU.RequestsServer.Domain.Exceptions;
using BSTU.RequestsServer.Domain.Models;
using BSTU.RequestsServer.Domain.PostgreSQL;
using BSTU.RequestsServer.Domain.RabbitMQ;

namespace BSTU.RequestsServer.Domain.Transactions
{
    public class RequestTransactionCommiter : IRequestTransactionCommiter
    {
        private const string InitialRequestAddedToDbSavepoint = "InitialRequestAdded";
        private const string SavePointNameKey = "SavePointName";

        private readonly RequestsContext _requestsContext;
        private readonly IRabbitMQClientWrapper _rabbitMqClient;

        public RequestTransactionCommiter(RequestsContext requestsContext, IRabbitMQClientWrapper rabbitMqClient)
        {
            _requestsContext = requestsContext;
            _rabbitMqClient = rabbitMqClient;
        }

        public async Task CommitTransactionAsync(Request request)
        {
            bool canConnect = await _requestsContext.Database.CanConnectAsync();
            if (!canConnect)
            {
                throw new ApplicationException("Cannot connect to the database.");
            }

            await using var requestTransaction = await _requestsContext.Database.BeginTransactionAsync();
            try
            {
                request.Status = RequestStatus.InProgress;
                _requestsContext.Requests.Add(request);
                await _requestsContext.SaveChangesAsync();

                await requestTransaction.CreateSavepointAsync(InitialRequestAddedToDbSavepoint);

                try
                {
                    request.Status = RequestStatus.Completed;
                    _rabbitMqClient.Send(request);
                    _requestsContext.Requests.Update(request);
                    await _requestsContext.SaveChangesAsync();
                    await requestTransaction.CommitAsync();
                }
                catch (Exception exception)
                {
                    exception.Data.Add(SavePointNameKey, InitialRequestAddedToDbSavepoint);
                    throw;
                }
            }
            catch (Exception exception)
            {
                string? savePointName = exception.Data.Contains(SavePointNameKey)
                    ? exception.Data[SavePointNameKey]?.ToString()
                    : null;

                if (string.IsNullOrWhiteSpace(savePointName))
                {
                    await requestTransaction.RollbackAsync();
                }
                else
                {
                    try
                    {
                        await requestTransaction.RollbackToSavepointAsync(savePointName);
                        request.Status = RequestStatus.Failed;
                        _requestsContext.Update(request);
                        await _requestsContext.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        await requestTransaction.RollbackAsync();
                        throw;
                    }
                }

                throw;
            }
        }
    }
}