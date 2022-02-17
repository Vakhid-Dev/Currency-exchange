using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurenncyExchange.Core
{
    public class AccountDetails :Account
    {
        public AccountDetails()
        {
            Id = Guid.NewGuid();
        }
        public decimal? Ammount { get; set; }
    }
}
