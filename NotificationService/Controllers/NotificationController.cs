using Microsoft.AspNetCore.Mvc;
using NotificationService.Storage;

namespace NotificationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    [HttpGet]
    public IActionResult GetLogs()
    {
        return Ok(EventLogStorage.EventLogs.ToList());
    }
}