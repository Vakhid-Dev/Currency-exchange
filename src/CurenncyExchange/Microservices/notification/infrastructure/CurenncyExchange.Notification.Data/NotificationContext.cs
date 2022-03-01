using CurenncyExchange.Notification.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurenncyExchange.Notification.Data
{
    public class NotificationContext : DbContext
    {
        public DbSet<Message?> Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ExchangeNotifications;Trusted_Connection=True;");
            }
        }
    }
}
