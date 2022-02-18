namespace CurenncyExchange.Core
{
    public class AccountDetails :Account
    {
        public AccountDetails()
        {
            Id = Guid.NewGuid();
        }
        public decimal? Ammount { get; set; }
    }
}
