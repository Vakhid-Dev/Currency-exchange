using System.ComponentModel.DataAnnotations.Schema;

namespace CurenncyExchange.Transaction.Core
{
    public class ByCurrencyRequest
    {
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

