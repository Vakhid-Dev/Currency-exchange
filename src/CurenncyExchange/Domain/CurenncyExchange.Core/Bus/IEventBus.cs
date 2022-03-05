using CurenncyExchange.Core.Events;

namespace CurenncyExchange.Core.Bus
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event) where TEvent : Event;
        void Subscribe<TEvent,IEventHandler> () where TEvent : Event where IEventHandler : IEventHandler<TEvent>;
    }
}
