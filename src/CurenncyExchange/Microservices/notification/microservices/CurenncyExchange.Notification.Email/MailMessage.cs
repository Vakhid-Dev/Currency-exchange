﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurenncyExchange.Notification.Email
{
    public class MailMessage
    {
        public MailMessage()
        {
            
        }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }

    }
}
