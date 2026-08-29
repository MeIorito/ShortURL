namespace ShortURL.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult GetHealthCheck()
    {
        return Ok();
    }
}