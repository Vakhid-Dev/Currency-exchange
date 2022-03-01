namespace CurenncyExchange.Core.RabbitMQ
{
    public interface IRabbitMqSender
    {
        public Task SendMessage(object message);
        public Task SendMessage(string message);
        
    }
}
