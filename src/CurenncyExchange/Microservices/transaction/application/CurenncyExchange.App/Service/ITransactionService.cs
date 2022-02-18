using CurenncyExchange.Core;

namespace CurenncyExchange.App.Service
{
    public interface ITransactionService
    {
        public Task ExecuteAsync(AccountDetails accountDetails);
      
    }
}
