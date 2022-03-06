using CurenncyExchange.Core.Bus;
using CurenncyExchange.TransactionCore.Commands;
using CurenncyExchange.TransactionCore.Events;
using MediatR;

namespace CurenncyExchange.TransactionCore.CommandsHandlers
{
    public class ByCurrencyCommandHandler : IRequestHandler<ByCurrencyCommand, bool>
    {
        private readonly IEventBus _eventBus;
        public ByCurrencyCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }
        public Task<bool> Handle(ByCurrencyCommand request, CancellationToken cancellationToken)
        {
            _eventBus.Publish(new BuyCurrencyEvent((Events.CurrencyType)request.CurrencyType, request.Rate, request.Ammount));
            return Task.FromResult(true);
        }
    }
}
