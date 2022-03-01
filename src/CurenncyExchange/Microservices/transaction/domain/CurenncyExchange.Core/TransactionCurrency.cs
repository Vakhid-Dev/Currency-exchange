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
        public DateTime DateCreation { get; set; }
        public Account? Accounts { get; set; }
        public CurrencyDetails? CurrencyDetails { get; set; }
    }
}
