using CurenncyExchange.Core;
using CurenncyExchange.Core.RabbitMQ;
using CurenncyExchange.Data.Context;
using CurenncyExchange.Transaction.Core;
using CurenncyExchange.Transaction.Core.Repository;
using RabbitMQ.Client;
using System.Text;
using System.Text;
using System.Text.Json;

namespace CurenncyExchange.App.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private TransactionContext _transactionContext;
        public TransactionRepository()
        {
            //    _transactionContext = transactionContext;
        }


        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            //  вынести значения "localhost" и "Queue"
            // в файл конфигурации
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("transaction", ExchangeType.Fanout);
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "transaction",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
               
            }
        }

    }

}

