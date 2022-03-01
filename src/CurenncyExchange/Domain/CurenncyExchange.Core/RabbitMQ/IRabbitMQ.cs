namespace CurenncyExchange.Core.RabbitMQ
{
    public interface IRabbitMQ
    {
        // public void SendMessage(object obj);
        public Task SendMessage(object message);
        public Task SendMessage(string message);
        
    }
}
