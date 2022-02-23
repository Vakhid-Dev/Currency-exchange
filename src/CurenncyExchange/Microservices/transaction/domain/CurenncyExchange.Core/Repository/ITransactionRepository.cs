using CurenncyExchange.Core;

namespace CurenncyExchange.Transaction.Core.Repository
{
    public interface ITransactionRepository
    {
      public  Task ExecuteAsync(TransactionCurrency transactionCurrency );
    }
}
