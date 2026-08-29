namespace ShortURL.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class UrlsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUrls()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetUrl(string id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateUrl()
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUrl(string id)
    {
        return Ok();
    }
}