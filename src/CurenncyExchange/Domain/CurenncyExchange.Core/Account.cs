namespace CurenncyExchange.Core
{
    public class Account
    {
        public Guid Id { get; set; }
        public int AccountBalance { get { return 1000; } }
        public Guid CurrencyDetailsId { get; set; }

    }
}
