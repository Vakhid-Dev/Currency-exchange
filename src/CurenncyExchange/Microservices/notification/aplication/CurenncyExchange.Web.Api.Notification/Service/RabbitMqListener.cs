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
    using CurenncyExchange.Notification.Core;
    using CurenncyExchange.Transaction.Core;
    using Newtonsoft.Json;
    using JsonSerializer = Newtonsoft.Json.JsonSerializer;
    using CurenncyExchange.Core;

    public class RabbitMqListener : BackgroundService, IRabbitMqSender, IRabbitMqReceiver
    {
        private IConnection _connection;
        private IModel _channel;
        private static string MessageFilePath = @"D:\Projects\Currency-exchange\src\CurenncyExchange\Microservices\notification\infrastructure\CurenncyExchange.Notification.Data\JsonData\Messages.json";

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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                stoppingToken.ThrowIfCancellationRequested();
                await Recieve();
            }
            catch (Exception)
            {

                throw;
            }
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
            transactionCurrency.Accounts = new Account()
            {
                CurrencyDetailsId = transactionCurrency.CurrencyDetails.Id,
                Id = new Guid()

            };
            var messageDto = new Message()
            {
                AccountId = transactionCurrency.Accounts.Id,
                Ammount = transactionCurrency.CurrencyDetails.Ammount,
                CurrencyType = transactionCurrency.CurrencyDetails.CurrencyType.ToString(),
                Rate = transactionCurrency.CurrencyDetails.Rate,
                IsSucces = true
            };
            string? message = JsonConvert.SerializeObject(messageDto);

            await SendMessage(message);

            await WriteToJsonFileAsync(messageDto);
        }

        public async Task WriteToJsonFileAsync(Message message)
        {
            await File.WriteAllTextAsync(MessageFilePath, JsonConvert.SerializeObject(message));


        }

        public async Task<Message> WriteToJsonFileAsync(string path)
        {
            path = MessageFilePath;
            Message message;
            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                message = await Task.Run<Message>(() => (Message)serializer.Deserialize(file, typeof(Message)));
            }
            return message;
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

        public async Task Recieve()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                TransactionCurrency? currency = JsonConvert.DeserializeObject<TransactionCurrency>(content);
                //TODO обрабатываем полученное сообщение в json file
            
                await SendMessage(currency);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("notification", false, consumer);
            await Task.Delay(1000);
        }
    }
}
