
using CurenncyExchange.Transaction.Core;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
namespace CurenncyExchange.Data.Context
{
    public class TransactionContext :DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext?> options)
             : base(options)    
        {

        }
        public DbSet <TransactionBase> TransactionDetails { get; set; }
      
    }
}
