using CurenncyExchange.Core.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurenncyExchange.TransactionCore.Events
{
    public class BuyCurrencyEvent  :Event
    {
        public BuyCurrencyEvent(CurrencyType currencyType,decimal rate,int amount)
        {
            CurrencyType = currencyType;
            Rate = rate;
            Ammount = amount;
        }
            public int Ammount { get; set; }

            [Column(TypeName = "decimal(18,2)")]
            public decimal Rate { get; set; }

            [Column(TypeName = "nvarchar(30)")]
            public CurrencyType CurrencyType { get; set; }

        }
        public enum CurrencyType
        {
            USD = 1,
            EURO = 2
        }
    }

