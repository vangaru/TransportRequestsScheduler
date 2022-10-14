using BSTU.RequestsServer.Domain.Models;

namespace BSTU.RequestsServer.Api.Handlers
{
    public interface IRequestsHandler
    {
        public void HandleRequest(Request request);
    }
}
