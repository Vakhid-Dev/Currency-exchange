using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurenncyExchange.Core.Events
{
    public abstract class Event
    {
        public Event()
        {
            Timestamp = DateTime.Now;
        }
     public DateTime Timestamp { get; protected set; }
    }
}
