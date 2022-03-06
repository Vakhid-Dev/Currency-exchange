using CurenncyExchange.Core.Bus;
using CurenncyExchange.Transaction.Core;
using CurenncyExchange.Transaction.Core.Repository;
using CurenncyExchange.TransactionCore.Commands;

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

        public void BuyingCurrencyAsync(ByCurrencyRequest byCurrencyRequest)
        {
            var byCurrencyCommand = new ByCurrencyCommand
            {
                Ammount = byCurrencyRequest.Ammount,
                CurrencyType = (TransactionCore.Commands.CurrencyType)byCurrencyRequest.CurrencyType,
                Rate = byCurrencyRequest.Rate,
           
             
            };
            _eventBus.SendComand(byCurrencyCommand);
        }
    }
}
