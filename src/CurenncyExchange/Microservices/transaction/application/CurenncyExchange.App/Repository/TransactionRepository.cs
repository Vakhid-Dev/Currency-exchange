using CurenncyExchange.Core;
using CurenncyExchange.Data.Context;
using CurenncyExchange.Transaction.Core;
using CurenncyExchange.Transaction.Core.Repository;
using RabbitMQ.Client;
using System.Text;

namespace CurenncyExchange.App.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private TransactionContext _transactionContext;
        public TransactionRepository()
        {
        //    _transactionContext = transactionContext;
        }
        //ToDo need to imlement 
        public async Task ExecuteAsync(TransactionCurrency transactionCurrency)
        {
            
            await PablishEvent(transactionCurrency);
           
        }
        public async Task<Task>PablishEvent(TransactionCurrency transactionCurrency) 
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                string message = "Transaction";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                if (transactionCurrency != null)
                {

                    using (var context = new TransactionContext())
                    {
                        await context.TransactionDetails.AddAsync(transactionCurrency);
                        await context.SaveChangesAsync();

                    }
                   
                }

            }  

            return Task.CompletedTask;
        }
    }
}
