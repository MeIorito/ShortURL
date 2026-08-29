namespace ShortURL.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/redirect")]
public class RedirectController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRedirect()
    {
        return Ok();
    }
}