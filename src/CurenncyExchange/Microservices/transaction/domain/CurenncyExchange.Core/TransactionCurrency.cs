using CurenncyExchange.Core;

namespace CurenncyExchange.Transaction.Core
{
    public class TransactionCurrency
    {
        public TransactionCurrency()
        {
            Id = new Guid();
        }
        public Guid Id { get; set; }
        public AccountDetails? AccountDetails { get; set; } 
    }
}
