namespace CurenncyExchange.Core
{
    public class Account
    {
        public Guid Id { get; set; }
        public int AccountBalance { get;private set; }
        public Guid CurrencyDetailsId { get; set; }

    }
}
