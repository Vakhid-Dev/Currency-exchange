using CurenncyExchange.Core;

namespace CurenncyExchange.Transaction.Core
{
    public class TransactionBase
    {
        public TransactionBase()
        {
            Id = new Guid();
        }
        public Guid Id { get; set; }
        public AccountDetails? AccountDetails { get; set; } 
    }
}
