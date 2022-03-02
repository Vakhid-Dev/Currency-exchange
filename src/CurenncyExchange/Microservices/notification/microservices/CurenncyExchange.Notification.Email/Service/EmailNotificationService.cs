using CurenncyExchange.Core.RabbitMQ;
using CurenncyExchange.Notification.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CurenncyExchange.Notification.Email.Service
{
    public class EmailNotificationService : INotificationService, IRabbitMqReceiver
    {

        public  void Notify(string content)
        {
            var mailMessage = new MailMessage()
            {
                From ="ExcangeService@gmail.com",
                To ="user@gmail.com",
                Subject= "Transaction",
                Body = content
            };
            Console.WriteLine($"Sented message to {mailMessage.To}");

        }

        public async Task Recieve()
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = connectionFactory.CreateConnection())
            using (var chanel = connection.CreateModel())
            {
                chanel.ExchangeDeclare("notification", ExchangeType.Direct);
                chanel.QueueDeclare("email");
                chanel.QueueBind("email", "notification", "email");
                var consumer = new EventingBasicConsumer(chanel);
                consumer.Received += (o, e) =>
                {
                    var body = e.Body.ToArray();
                    var encodingMessage = Encoding.UTF8.GetString(body);
                    Notify(encodingMessage);
                    Console.WriteLine(e.Exchange);
                    Console.WriteLine(e.RoutingKey);
                    Console.WriteLine("Reciving [message] {0} ", encodingMessage);
                };
                chanel.BasicConsume("email", true, consumer);
                Console.ReadLine();
            }
            await Task.Delay(1000);
        }
    }
}
