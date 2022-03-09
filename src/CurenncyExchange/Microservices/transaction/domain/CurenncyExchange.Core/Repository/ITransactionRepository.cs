using CurenncyExchange.Core;
using CurenncyExchange.Core.RabbitMQ;
using CurenncyExchange.TransactionCore.Commands;

namespace CurenncyExchange.Transaction.Core.Repository
{
    public interface ITransactionRepository 
    {
      public Task BuyingCurrencyAsync(ByCurrencyCommand  byCurrencyCommand);
       
    }
}
