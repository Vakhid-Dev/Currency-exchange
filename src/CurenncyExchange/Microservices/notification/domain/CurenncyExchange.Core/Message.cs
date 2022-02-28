using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurenncyExchange.Core
{
    public class Message
    {
        public int Id { get; set; }
        public bool IsSucces { get; set; }
        public decimal? Ammount { get; set; }
        public string? CurrencyType { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AccountId { get; set; }
    }
}
