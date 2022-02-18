using CurenncyExchange.Core;
using CurenncyExchange.Data.Context;
using CurenncyExchange.Transaction.Core.Repository;
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
    }
}
