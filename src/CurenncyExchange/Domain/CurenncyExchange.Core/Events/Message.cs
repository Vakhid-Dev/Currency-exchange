using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace CurenncyExchange.Core.Events
{
    public abstract class Message : IRequest<bool>
    {
        protected Message()
        {
            Type = GetType().Name;
        }

        public string Type { get; protected set; }
    }
}
