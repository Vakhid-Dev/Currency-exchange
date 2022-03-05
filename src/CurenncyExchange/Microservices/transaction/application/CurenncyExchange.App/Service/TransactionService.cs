using CurenncyExchange.Core.Bus;
using CurenncyExchange.Transaction.Core;
using CurenncyExchange.Transaction.Core.Repository;

namespace CurenncyExchange.App.Service
{
    public class TransactionService :ITransactionService
    {
        private ITransactionRepository _transactionRepository;
        private readonly IEventBus _eventBus;
        public TransactionService(ITransactionRepository transactionRepository, IEventBus eventBus)
        {
            _transactionRepository = transactionRepository;
            _eventBus = eventBus;
        }
        //ToDo need to imlement 
        public async Task BuyingCurrencyAsync(TransactionCurrency transactionCurrency)
        {
           await _transactionRepository.BuyingCurrencyAsync(transactionCurrency).ConfigureAwait(false);
             
        }

        public Task BuyingCurrencyAsync(ByCurrencyRequest byCurrencyRequest)
        {
            throw new NotImplementedException();
        }
    }
}
