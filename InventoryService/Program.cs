using InventoryService.Consumers;
using MassTransit;
using MassTransit.JobService.Scheduling;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductAddedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");

        cfg.ReceiveEndpoint("inventory-service", e =>
        {
            e.ConfigureConsumer<ProductAddedConsumer>(context);

            e.UseMessageRetry(r =>
            {
                r.Interval(3, TimeSpan.FromSeconds(5));
            });

            //e.DeadLetterExchange = "inventory-service-dlq";
            //e.BindDeadLetterQueue("inventory-service-dlq");
            //e.dead
            //e.usede
            //e.ConfigureDeadLetter(dl =>
            //{
            //    dl. "inventory-service-dlq";
            //    dl.QueueName = "inventory-service-dlq";

            //    dl.RethrowFaultedMessages();
            //    dl.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(30)));
            //});
        });
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();