namespace ShortURL.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortURL.DTOs;
using ShortURL.Services;

[ApiController]
[Route("api/v1/urls")]
public class UrlsController : ControllerBase
{
    private readonly UrlService _urlService;

    public UrlsController(UrlService urlService)
    {
        _urlService = urlService;
    }

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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateUrl(CreateUrlDto createDto)
    {
        CreateUrlResponseDto dto = await _urlService.CreateUrlAsync(createDto);

        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUrl(string id)
    {
        return Ok();
    }
}