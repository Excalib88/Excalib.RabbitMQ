using Excalib.RabbitMQ.Models;
using MassTransit;

namespace Excalib.RabbitMQ.Consumers;

public class CreateTransactionConsumer : IConsumer<CreateTransaction>
{
    private readonly ILogger<CreateTransactionConsumer> _logger;

    public CreateTransactionConsumer(ILogger<CreateTransactionConsumer> logger)
    {
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<CreateTransaction> context)
    {
        _logger.LogInformation($"Consume create transaction {context.Message.CardNumber} amount {context.Message.Amount}");
        //todo: do something with data
    }
}