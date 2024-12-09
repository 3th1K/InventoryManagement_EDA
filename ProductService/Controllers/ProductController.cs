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
        var productId = Guid.NewGuid();

        await _publishEndpoint.Publish(new ProductAdded(productId, request.Name));

        return Ok(new { ProductId = productId, Message = "Product added successfully!" });
    }
}

public record AddProductRequest(string Name);