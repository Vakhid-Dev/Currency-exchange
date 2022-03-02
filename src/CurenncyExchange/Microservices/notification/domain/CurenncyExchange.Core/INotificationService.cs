using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurenncyExchange.Notification.Core
{
    public interface INotificationService
    {
        public void Notify(string content);
    }
}
