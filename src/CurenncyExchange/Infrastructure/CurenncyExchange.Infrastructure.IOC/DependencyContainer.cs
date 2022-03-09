using CurenncyExchange.App.Service;
using CurenncyExchange.Core.Bus;
using CurenncyExchange.Infrastructure.Bus;
using CurenncyExchange.TransactionCore.Commands;
using CurenncyExchange.TransactionCore.CommandsHandlers;
using CurenncyExchange.TransactionCore.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurenncyExchange.Infrastructure.IOC
{
    public class DependencyContainer
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Domain bus.
            services.AddSingleton<IEventBus, RabbitMQBus>(serviceProvider => {
                var mediator = serviceProvider.GetService<IMediator>();
                var serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
                return new RabbitMQBus(mediator, serviceScopeFactory);
            });
            // Services.
            services.AddTransient<ITransactionService, TransactionService>();
         
            // Commands
            services.AddTransient<IRequestHandler<ByCurrencyCommand, bool>, ByCurrencyCommandHandler>();



  
        }
    }
}
