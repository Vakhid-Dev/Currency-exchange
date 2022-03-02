using CurenncyExchange.Core.RabbitMQ;
using CurenncyExchange.Notification.Core;
using CurenncyExchange.Notification.Sms;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CurenncyExchange.Notification.Email.Service
{
    public class SmsNotificationService : INotificationService, IRabbitMqReceiver
    {
        public async Task Recieve()
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
                    Notify(encodingMessage);
                    Console.WriteLine(e.Exchange);
                    Console.WriteLine(e.RoutingKey);
                    Console.WriteLine("Reciving [message] {0} ", encodingMessage);
                };
                chanel.BasicConsume("sms", true, consumer);
                Console.ReadLine();
            }
        }


        public void Notify(string content)
        {
            var phone = new Phone()
            {
                PhoneNumber = RandomDigits(10),
                Body = content
            };
            Console.WriteLine($"Sented message to {phone.PhoneNumber}");

        }
        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
