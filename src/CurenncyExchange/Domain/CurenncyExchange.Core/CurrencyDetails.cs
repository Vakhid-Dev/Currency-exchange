using System.ComponentModel.DataAnnotations.Schema;

namespace CurenncyExchange.Core
{
    public class CurrencyDetails
    {
        public Guid Id { get; set; }
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
