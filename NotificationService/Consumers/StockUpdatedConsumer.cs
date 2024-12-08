using MassTransit;
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

        _logger.LogInformation($"Stock updated for Product {stockUpdate.ProductId}: {stockUpdate.Quantity} units.");

        return Task.CompletedTask;
    }
}