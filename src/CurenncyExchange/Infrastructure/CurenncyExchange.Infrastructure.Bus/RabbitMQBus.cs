using CurenncyExchange.Core.Bus;
using CurenncyExchange.Core.Commands;
using CurenncyExchange.Core.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CurenncyExchange.Infrastructure.Bus
{
    public class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private static IConnection _rabbitMqConnection;
        private static IModel _rabbitMqChannel;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            _rabbitMqConnection = _rabbitMqConnection ?? factory.CreateConnection();
            _rabbitMqChannel = _rabbitMqChannel ?? _rabbitMqConnection.CreateModel();
            var eventName = typeof(TEvent).Name;
            _rabbitMqChannel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: true, arguments: null);
            var message = JsonConvert.SerializeObject(@event);
            var messageBody = Encoding.UTF8.GetBytes(message);
            _rabbitMqChannel.BasicPublish(exchange: string.Empty, routingKey: eventName, basicProperties: null, body: messageBody);

        }
        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventType = typeof(TEvent);
            var eventName = eventType.Name;
            var eventHandlerType = typeof(TEventHandler);
            if (!_eventTypes.Contains(eventType))
            {
                _eventTypes.Add(eventType);
            }
            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }
            if (_handlers[eventName].Any(x => x.GetType() == eventHandlerType))
            {
                throw new ArgumentException($"Event handler type '{eventHandlerType}' is already registered for event '{eventName}'", nameof(eventHandlerType));
            }
            _handlers[eventName].Add(eventHandlerType);
        }
        private void StartBasicConsume<TEvent>() where TEvent : Event
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                DispatchConsumersAsync = true
            };
            _rabbitMqConnection = _rabbitMqConnection ?? factory.CreateConnection();
            _rabbitMqChannel = _rabbitMqChannel ?? _rabbitMqConnection.CreateModel();
            var eventName = typeof(TEvent).Name;
            _rabbitMqChannel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: true, arguments: null);
            var consumer = new AsyncEventingBasicConsumer(_rabbitMqChannel);
            consumer.Received += ConsumerReceived;
            _rabbitMqChannel.BasicConsume(queue: eventName, autoAck: true, consumer);
        }
        private async Task ConsumerReceived(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task ProcessEvent(string eventName, string message)
        {
            if (!_handlers.ContainsKey(eventName))
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var subscriptions = _handlers[eventName];
                foreach (var subscription in subscriptions)
                {
                    var handlerInstance = scope.ServiceProvider.GetService(subscription);
                    if (handlerInstance == null)
                    {
                        continue;
                    }
                    var eventType = _eventTypes.SingleOrDefault(x => x.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);
                    var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                    await (Task)concreteType.GetMethod(nameof(IEventHandler<Event>.Handle)).Invoke(handlerInstance, new object[] { @event });
                }
            }
        }

        public Task SendComand<TComand>(TComand comand) where TComand : Command
        {
          return _mediator.Send(comand);
        }
    }
}
