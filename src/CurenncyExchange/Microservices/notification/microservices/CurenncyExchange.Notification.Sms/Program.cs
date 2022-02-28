using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class Program
{
    static void Main(string[] args)
    {
        var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = connectionFactory.CreateConnection())
        using (var chanel = connection.CreateModel())
        {
            chanel.ExchangeDeclare("notification", ExchangeType.Direct);
            chanel.QueueDeclare("sms");
            chanel.QueueBind("sms", "notification", "sms");
            var consumer = new EventingBasicConsumer(chanel);
            consumer.Received += (o, e) =>
            {
                var body = e.Body.ToArray();
                var encodingMessage = Encoding.UTF8.GetString(body);
                Console.WriteLine(e.Exchange);
                Console.WriteLine(e.RoutingKey);
                Console.WriteLine("Reciving [message] {0} ", encodingMessage);
            };
            chanel.BasicConsume("sms", true, consumer);
            Console.ReadLine();
        }
    }
}