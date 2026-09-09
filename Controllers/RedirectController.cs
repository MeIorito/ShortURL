namespace ShortURL.Controllers;
using Microsoft.AspNetCore.Mvc;
using ShortURL.Services;

[ApiController]
[Route("api/v1/redirect")]
public class RedirectController : ControllerBase
{
    private readonly UrlService _urlService;

    public RedirectController(UrlService urlService)
    {
        _urlService = urlService;
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetRedirect(string code)
    {
        string originalUrl = await _urlService.GetOriginalUrl(code);

        return Redirect(originalUrl);
    }
}