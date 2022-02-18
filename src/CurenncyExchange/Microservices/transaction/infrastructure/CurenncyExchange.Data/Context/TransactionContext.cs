using CurenncyExchange.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
