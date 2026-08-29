namespace ShortURL.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/clicks")]
public class ClicksController : ControllerBase
{
    [HttpGet]
    public IActionResult GetClicks(string id)
    {
        return Ok();
    }

    [HttpGet]
    public IActionResult GetClick(string id)
    {
        return Ok();
    }
}