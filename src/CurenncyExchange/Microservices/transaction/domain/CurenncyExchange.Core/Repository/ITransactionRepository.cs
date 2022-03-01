using CurenncyExchange.Core;
using CurenncyExchange.Core.RabbitMQ;

namespace CurenncyExchange.Transaction.Core.Repository
{
    public interface ITransactionRepository : IRabbitMQ
    {
      public Task BuyingCurrencyAsync(TransactionCurrency currency);
    }
}
