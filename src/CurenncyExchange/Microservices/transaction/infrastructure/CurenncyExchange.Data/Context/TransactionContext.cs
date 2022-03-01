using CurenncyExchange.Transaction.Core;
using Microsoft.EntityFrameworkCore;
namespace CurenncyExchange.Data.Context
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }
        public DbSet<TransactionCurrency> TransactionDetails { get; set; }

    }
}
