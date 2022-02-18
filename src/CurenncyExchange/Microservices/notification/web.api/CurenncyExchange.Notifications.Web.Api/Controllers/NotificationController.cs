using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurenncyExchange.Notifications.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public Task ReadAsync() 
        {
            return Task.CompletedTask;
        }
        public Task PublisAsync()
        {
            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.ExchangeDeclare("transaction",
            //                             ExchangeType.Fanout);

            //    channel.QueueDeclare(queue: "notification",
            //                         durable: false,
            //                         exclusive: false,
            //                         autoDelete: false,
            //                         arguments: null);

            //    channel.QueueDeclare(queue: "tracing",
            //                         durable: false,
            //                         exclusive: false,
            //                         autoDelete: false,
            //                         arguments: null);

            //    string message = "Transaction";
            //    var body = Encoding.UTF8.GetBytes(message);

            //    channel.BasicPublish(exchange: "transaction",
            //                         routingKey: "",
            //                         basicProperties: null,
            //                         body: body);

            //}
            return Task.CompletedTask;
        }
    }
}
