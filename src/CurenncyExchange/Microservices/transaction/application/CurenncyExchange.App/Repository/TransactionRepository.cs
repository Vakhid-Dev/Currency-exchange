using CurenncyExchange.Data.Context;
using CurenncyExchange.Transaction.Core;
using CurenncyExchange.Transaction.Core.Repository;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CurenncyExchange.App.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private TransactionContext _transactionContext;
        public TransactionRepository(TransactionContext transactionContext)
        {
            _transactionContext = transactionContext;
        }

        public async Task BuyingCurrencyAsync(TransactionCurrency currency)
        {
            if (currency == null)
            {
                throw new ArgumentNullException($"{nameof(BuyingCurrencyAsync)} entity must not be null");
            }
            using (var transaction = await _transactionContext.Database.BeginTransactionAsync())
            {

                try
                {

                    await _transactionContext.TransactionDetails.AddAsync(currency);
                    await SendMessage(currency);
                    await _transactionContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                }
                catch (Exception)
                {
                    //log need here
                    await transaction.RollbackAsync();
                }


            }
        }


        public async Task SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            await SendMessage(message);
        }

        public Task SendMessage(string message)
        {
            //ToDo вынести значения "localhost" и "queue" в файл конфигурации
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
            return Task.CompletedTask;
        }

    }

}

