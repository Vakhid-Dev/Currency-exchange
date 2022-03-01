namespace CurenncyExchange.Web.Api.Notification.Service
{
    using RabbitMQ.Client.Events;
    using RabbitMQ.Client;
    using System.Threading.Tasks;
    using System.Threading;
    using Microsoft.Extensions.Hosting;
    using System.Text;
    using System;
    using CurenncyExchange.Core.RabbitMQ;
    using System.Text.Json;
    using CurenncyExchange.Notification.Core;
    using CurenncyExchange.Transaction.Core;

    public class RabbitMqListener : BackgroundService, IRabbitMQ
    {
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqListener()
        {
            // Не забудьте вынести значения "localhost" и "Queue"
            // в файл конфигурации
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("transaction", ExchangeType.Fanout);
            _channel.QueueDeclare("notification");
            _channel.QueueBind(queue: "notification", "transaction", "");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                // Каким-то образом обрабатываем полученное сообщение
                Console.WriteLine($"Получено сообщение: {content}");
                Console.WriteLine("_____________________________");
                await SendMessage(content);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("notification", false, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }


        public async Task SendMessage(object obj)
        {
            TransactionCurrency? transactionCurrency = obj as TransactionCurrency;
            var messageDto = new Message()
            {
                AccountId = transactionCurrency.Accounts.Id,
                Ammount = transactionCurrency.CurrencyDetails.Ammount,
                CurrencyType = transactionCurrency.CurrencyDetails.CurrencyType.ToString(),
                Rate = transactionCurrency.CurrencyDetails.Rate,
                IsSucces = true,
                Id = new Guid()
            };
            var message = JsonSerializer.Serialize(messageDto);
            await SendMessage(message);
        }

        public async Task SendMessage(string message)
        {
            //  вынести значения "localhost" и "Queue"
            // в файл конфигурации
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("notification", ExchangeType.Direct);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "notification",
                                 routingKey: "email",
                                 basicProperties: null,
                                 body: body);
                channel.BasicPublish(exchange: "notification",
                               routingKey: "sms",
                               basicProperties: null,
                               body: body);
            }
            await Task.Delay(1000);
        }

    }
}
