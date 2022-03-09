using CurenncyExchange.Core;
using CurenncyExchange.Data.Context;
using CurenncyExchange.Transaction.Core;
using CurenncyExchange.Transaction.Core.Repository;
using CurenncyExchange.TransactionCore.Commands;
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

   

        public async Task BuyingCurrencyAsync(ByCurrencyCommand byCurrencyCommand)
        {
            if (byCurrencyCommand == null)
            {
                throw new ArgumentNullException($"{nameof(BuyingCurrencyAsync)} entity must not be null");
            }
            using (var transaction = await _transactionContext.Database.BeginTransactionAsync())
            {

                try
                {
                    TransactionCurrency transactionCurrency = new TransactionCurrency()
                    {
                        CurrencyDetails = ToCurrencyDetails(byCurrencyCommand)
                    };

                    await _transactionContext.TransactionDetails.AddAsync(transactionCurrency);
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
        
        public static CurrencyDetails ToCurrencyDetails(ByCurrencyCommand byCurrencyCommand)
        {
            return new CurrencyDetails
            {
                Ammount = byCurrencyCommand.Ammount,
                CurrencyType = (Core.CurrencyType)byCurrencyCommand.CurrencyType,
                Rate = byCurrencyCommand.Rate
            };
        }


    }

}

