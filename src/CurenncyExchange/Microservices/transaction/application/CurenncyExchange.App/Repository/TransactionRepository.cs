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
        public TransactionRepository(TransactionContext transactionContext)
        {
            _transactionContext = transactionContext;
        }
        //ToDo need to imlement 
        public async Task ExecuteAsync(AccountDetails accountDetails)
        {
            
            await PablishEvent(accountDetails);
           
        }
        public async Task<Task>PablishEvent(AccountDetails accountDetails) 
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
                if (accountDetails!=null)
                {
                   var transaction = new TransactionBase()
                    {
                        AccountDetails = accountDetails
                    };
                   await _transactionContext.TransactionDetails.AddAsync(transaction);
                   await _transactionContext.SaveChangesAsync();
                }

            }  

            return Task.CompletedTask;
        }
    }
}
