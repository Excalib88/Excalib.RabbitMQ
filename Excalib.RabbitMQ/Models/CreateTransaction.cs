namespace Excalib.RabbitMQ.Models;

public class CreateTransaction
{
    public string CardNumber { get; set; } = null!;
    public decimal Amount { get; set; }
}