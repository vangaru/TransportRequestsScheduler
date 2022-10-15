using BSTU.RequestsServer.Domain.Models;
using BSTU.RequestsServer.Domain.RabbitMQ;

namespace BSTU.RequestsServer.Api.Handlers
{
    public class RequestsHandler : IRequestsHandler
    {
        private readonly ILogger<RequestsHandler> _logger;
        private readonly IRabbitMQClientWrapper _rabbitMqClient;

        public RequestsHandler(ILogger<RequestsHandler> logger, IRabbitMQClientWrapper rabbitMqClient)
        {
            _logger = logger;
            _rabbitMqClient = rabbitMqClient;
        }

        public void HandleRequest(Request request)
        {
            if (request.DateTime == default(DateTime))
            {
                request.DateTime = DateTime.Now;
            }
            _rabbitMqClient.Send(request);
            _logger.LogInformation(request.ToString());
        }
    }
}