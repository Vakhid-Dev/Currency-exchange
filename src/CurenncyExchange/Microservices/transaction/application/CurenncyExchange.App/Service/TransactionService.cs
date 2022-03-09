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
        public TransactionService(IEventBus eventBus, ITransactionRepository transactionRepository)
        {
            _eventBus = eventBus;
            _transactionRepository = transactionRepository;
        }

        public async Task BuyingCurrencyAsync(ByCurrencyRequest byCurrencyRequest)
        {
            ByCurrencyCommand? byCurrencyCommand = new ByCurrencyCommand
            {
                Ammount = byCurrencyRequest.Ammount,
                CurrencyType = (TransactionCore.Commands.CurrencyType)byCurrencyRequest.CurrencyType,
                Rate = byCurrencyRequest.Rate,          
             
            };
            await _eventBus.SendCommand(byCurrencyCommand);
            await _transactionRepository.BuyingCurrencyAsync(byCurrencyCommand).ConfigureAwait(false);
           
        }
    }
}
