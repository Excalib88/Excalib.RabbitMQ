using Excalib.RabbitMQ;
using Excalib.RabbitMQ.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<RabbitBgWorker>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<NotifyTransactionConsumer>();
    x.AddConsumer<CreateTransactionConsumer>();
    
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", c =>
        {
            c.Username("root");
            c.Password("123");
        });
        
        cfg.ReceiveEndpoint("NotifyTransactionsQueue", e =>
        {
            e.ConfigureConsumer<NotifyTransactionConsumer>(context);
        });
        cfg.ReceiveEndpoint("CreateTransactionsQueue", e =>
        {
            e.ConfigureConsumer<CreateTransactionConsumer>(context);
        });
        
        cfg.ClearSerialization();
        cfg.UseRawJsonSerializer();
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();