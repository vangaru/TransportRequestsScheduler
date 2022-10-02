using BSTU.RequestsServer.Api.Models;

namespace BSTU.RequestsServer.Api.Handlers
{
    public interface IRequestsHandler
    {
        public Task HandleRequest(Request request);
    }
}
