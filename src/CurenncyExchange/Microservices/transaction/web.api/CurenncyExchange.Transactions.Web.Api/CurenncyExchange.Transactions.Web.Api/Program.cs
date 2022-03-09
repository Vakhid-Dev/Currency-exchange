using CurenncyExchange.App.Repository;
using CurenncyExchange.App.Service;
using CurenncyExchange.Data.Context;
using CurenncyExchange.Transaction.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MediatR;
using CurenncyExchange.TransactionCore.Commands;
using CurenncyExchange.TransactionCore.CommandsHandlers;
using CurenncyExchange.Core.Bus;
using CurenncyExchange.Infrastructure.Bus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.22тттт

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Domain bus.
builder.Services.AddSingleton<IEventBus, RabbitMQBus>(serviceProvider => {
    var mediator = serviceProvider.GetService<IMediator>();
    var serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
    return new RabbitMQBus(mediator, serviceScopeFactory);
});
builder.Services.AddDbContext<TransactionContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TransactionDb"));
});

builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddTransient<IRequestHandler<ByCurrencyCommand, bool>, ByCurrencyCommandHandler>();
builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
