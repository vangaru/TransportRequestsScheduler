using BSTU.RequestsServer.Domain.Models;

namespace BSTU.RequestsServer.Domain.RabbitMQ
{
    public interface IRabbitMQClientWrapper
    {
        public void Send(Request request);
    }
}
