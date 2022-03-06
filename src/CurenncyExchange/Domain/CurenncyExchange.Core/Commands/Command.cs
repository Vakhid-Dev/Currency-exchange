using CurenncyExchange.Core.Events;
namespace CurenncyExchange.Core.Commands
{
    public class Command :Message
    {
        protected Command()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; protected set; }
    }
}
