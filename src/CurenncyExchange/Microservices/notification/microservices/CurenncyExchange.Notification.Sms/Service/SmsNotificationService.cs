﻿using CurenncyExchange.Notification.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CurenncyExchange.Notification.Email.Service
{
    public class SmsNotificationService : INotificationService
    {
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
                    Console.WriteLine(e.Exchange);
                    Console.WriteLine(e.RoutingKey);
                    Console.WriteLine("Reciving [message] {0} ", encodingMessage);
                };
                chanel.BasicConsume("email", true, consumer);
                Console.ReadLine();
            }
        }
        public void Notify(Guid subjectId, Message message)
        {
            throw new NotImplementedException();
        }
    }
}
