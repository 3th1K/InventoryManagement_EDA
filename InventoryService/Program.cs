using InventoryService.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
// Add services to the container.
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductAddedConsumer>(); // Register the consumer

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost"); // RabbitMQ URL

        // Configure the consumer
        cfg.ReceiveEndpoint("inventory-service", e =>
        {
            e.ConfigureConsumer<ProductAddedConsumer>(context);
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