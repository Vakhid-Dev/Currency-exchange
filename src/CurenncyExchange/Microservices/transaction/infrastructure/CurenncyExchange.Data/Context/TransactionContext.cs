
using CurenncyExchange.Transaction.Core;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
namespace CurenncyExchange.Data.Context
{
    public class TransactionContext :DbContext
    {
        public TransactionContext()    
        {
        
        }
        public DbSet <TransactionCurrency> TransactionDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ExchangeTransactions;Trusted_Connection=True;");
            }
        }
    }
}
