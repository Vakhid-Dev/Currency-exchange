namespace CurenncyExchange.Core.RabbitMQ
{
    public interface IRabbitMQ
    {
        public void SendMessage(object obj);
        public void SendMessage(string message);
    }
}
