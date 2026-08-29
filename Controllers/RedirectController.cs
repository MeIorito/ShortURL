namespace ShortURL.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class RedirectController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRedirect()
    {
        return Ok();
    }
}