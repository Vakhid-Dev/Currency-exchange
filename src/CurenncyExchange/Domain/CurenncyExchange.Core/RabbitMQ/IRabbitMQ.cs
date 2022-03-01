namespace CurenncyExchange.Core.RabbitMQ
{
    public interface IRabbitMQ
    {
        public Task SendMessage(object message);
        public Task SendMessage(string message);
        
    }
}
