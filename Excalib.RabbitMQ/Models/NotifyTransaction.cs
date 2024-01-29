namespace Excalib.RabbitMQ.Models;

public class NotifyTransaction
{
    public string CardNumber { get; set; } = null!;
    public decimal Amount { get; set; }
}