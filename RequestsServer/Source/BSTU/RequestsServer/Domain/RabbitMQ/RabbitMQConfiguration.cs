namespace BSTU.RequestsServer.Domain.RabbitMQ
{
    public class RabbitMQConfiguration
    {
        public string? QueueName { get; set; }
        public string? HostName { get; set; }
        public string? VirtualHost { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? Port { get; set; }
    }
}