using Excalib.RabbitMQ.Models;
using MassTransit;

namespace Excalib.RabbitMQ.Consumers;

public class NotifyTransactionConsumer : IConsumer<NotifyTransaction>
{
    private readonly ILogger<NotifyTransactionConsumer> _logger;

    public NotifyTransactionConsumer(ILogger<NotifyTransactionConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotifyTransaction> context)
    {
        _logger.LogInformation($"Consume notify about transaction {context.Message.CardNumber} amount {context.Message.Amount}");
        //todo: do something with data
    }
}