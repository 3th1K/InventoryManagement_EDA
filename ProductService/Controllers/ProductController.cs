using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;

namespace ProductService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
    {
        // Simulate saving product to a database (in-memory example)
        var productId = Guid.NewGuid();

        // Publish the ProductAdded event
        await _publishEndpoint.Publish(new ProductAdded(productId, request.Name));

        return Ok(new { ProductId = productId, Message = "Product added successfully!" });
    }
}

// DTO for incoming request
public record AddProductRequest(string Name);