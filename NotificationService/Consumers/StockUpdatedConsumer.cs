using MassTransit;
using NotificationService.Storage;
using Shared.Events;

namespace NotificationService.Consumers;

public class StockUpdatedConsumer : IConsumer<StockUpdated>
{
    private readonly ILogger<StockUpdatedConsumer> _logger;

    public StockUpdatedConsumer(ILogger<StockUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<StockUpdated> context)
    {
        var stockUpdate = context.Message;

        var logEntry = $"Stock updated for Product {stockUpdate.ProductId}: {stockUpdate.Quantity} units.";

        _logger.LogInformation(logEntry);
        EventLogStorage.AddEventLog(logEntry);

        return Task.CompletedTask;
    }
}