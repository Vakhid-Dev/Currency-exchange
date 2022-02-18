using CurenncyExchange.Core;
using CurenncyExchange.Data.Context;
using CurenncyExchange.Transaction.Core.Repository;
using RabbitMQ.Client;
using System.Text;

namespace CurenncyExchange.App.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private TransactionContext _transactionContext;
        public TransactionRepository(TransactionContext transactionContext)
        {
            _transactionContext = transactionContext;
        }
        //ToDo need to imlement 
        public Task ExecuteAsync(AccountDetails accountDetails)
        {
            return Task.CompletedTask;
        }
        public Task PablishEvent() 
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("transaction",
                                         ExchangeType.Fanout);

                channel.QueueDeclare(queue: "notification",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.QueueDeclare(queue: "tracing",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Transaction";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "transaction",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
               
            }  

            return Task.CompletedTask;
        }
    }
}
