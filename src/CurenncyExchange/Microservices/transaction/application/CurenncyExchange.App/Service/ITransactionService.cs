using CurenncyExchange.Transaction.Core;

namespace CurenncyExchange.App.Service
{
    public interface ITransactionService
    {
        public Task ExecuteAsync(TransactionCurrency transactionCurrency);
      
    }
}
