using MassTransit;
using Shared.Events;

namespace InventoryService.Consumers;

public class ProductAddedConsumer : IConsumer<ProductAdded>
{
    private readonly ILogger<ProductAddedConsumer> _logger;

    public ProductAddedConsumer(ILogger<ProductAddedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProductAdded> context)
    {
        var product = context.Message;

        _logger.LogInformation($"Received ProductAdded event: {product.ProductId} - {product.Name}");

        int initialStock = 0;
        _logger.LogInformation($"Stock initialized: {initialStock} units for Product {product.Name}");

        await context.Publish(new StockUpdated(product.ProductId, initialStock));
    }
}