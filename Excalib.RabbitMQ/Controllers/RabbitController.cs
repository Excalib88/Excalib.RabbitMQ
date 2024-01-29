using System.Text;
using System.Text.Json;
using Excalib.RabbitMQ.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Excalib.RabbitMQ.Controllers;

[ApiController]
[Route("rabbit")]
public class RabbitController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("send")]
    public IActionResult Send(object message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "TestQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(exchange: "",
                routingKey: "TestQueue",
                basicProperties: null,
                body: body);
        }
        return Ok();
    }

    [HttpPost("transactions/notify")]
    public async Task<IActionResult> SendNotify(NotifyTransaction transaction)
    {
        await _publishEndpoint.Publish(transaction);
        
        return Ok();
    }

    [HttpPost("transactions/create")]
    public async Task<IActionResult> SendTransactions(CreateTransaction transaction)
    {
        await _publishEndpoint.Publish(transaction);

        return Ok();
    }
}