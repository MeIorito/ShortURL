namespace ShortURL.Controllers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortURL.DTOs;
using ShortURL.Services;

[ApiController]
[Route("api/v1/urls")]
public class UrlsController : ControllerBase
{
    private readonly UrlService _urlService;
    private readonly UserContextService _userContextService;

    public UrlsController(UrlService urlService, UserContextService userContextService)
    {
        _urlService = urlService;
        _userContextService = userContextService;
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
        Guid userId = await _userContextService.GetCurrentUserSub();

        CreateUrlResponseDto dto = await _urlService.CreateUrlAsync(createDto, userId);

        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUrl(string id)
    {
        return Ok();
    }
}