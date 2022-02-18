using CurenncyExchange.Core;
using Microsoft.EntityFrameworkCore;

namespace CurenncyExchange.Data.Context
{
    public class TransactionContext :DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext?> options)
             : base(options)    
        {

        }
        public DbSet <AccountDetails> AccountDetails { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
