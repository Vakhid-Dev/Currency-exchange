using CurenncyExchange.Core;
using CurenncyExchange.Transaction.Core;
using CurenncyExchange.Transaction.Core.Repository;

namespace CurenncyExchange.App.Service
{
    public class TransactionService :ITransactionService
    {
        private ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        //ToDo need to imlement 
        public Task ExecuteAsync (TransactionCurrency transactionCurrency)
        {
            _transactionRepository.SendMessage(transactionCurrency);
            return Task.CompletedTask;  
        }
    }
}
