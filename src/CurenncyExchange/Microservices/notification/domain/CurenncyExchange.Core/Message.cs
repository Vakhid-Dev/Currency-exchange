﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurenncyExchange.Notification.Core
{
    public class Message
    {
        public Guid Id { get; set; }
        public bool IsSucces { get; set; }
        public decimal? Ammount { get; set; }
        public string? CurrencyType { get; set; }
        public decimal? Rate { get; set; }
        public Guid AccountId { get; set; }
    }
}
