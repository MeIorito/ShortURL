namespace ShortURL.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortURL.DTOs;
using ShortURL.Enums;
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

    // Test endpoint for debugging, not for real world usage
    [HttpGet]
    public async Task<IActionResult> GetUrls()
    {
        GetAllUrlsResponseDto dto = await _urlService.GetAllUrlsAsync();

        return Ok(dto);
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUrlsByUserIdAdmin(Guid userId)
    {
        GetAllUrlsResponseDto dto = await _urlService.GetAllUrlsByUserId(userId);

        return Ok(dto);
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> GetUrlsByUserId()
    {
        Guid userId = await _userContextService.GetCurrentUserSub();
        GetAllUrlsResponseDto dto = await _urlService.GetAllUrlsByUserId(userId);

        return Ok(dto);
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
        UserTier role = await _userContextService.GetCurrentUserTier();

        CreateUrlResponseDto dto = await _urlService.CreateUrlAsync(createDto, userId, role);

        return Ok(dto);
    }

    [HttpPost("free")]
    public async Task<IActionResult> CreateUrlFree(CreateUrlDto createDto)
    {
        CreateUrlResponseDto dto = await _urlService.CreateUrlAsync(createDto, null, UserTier.Anonymous);

        return Ok(dto);
    }

    [Authorize]
    [HttpDelete("{urlId}")]
    public async Task<IActionResult> DeleteUrl(Guid urlId)
    {
        Guid userId = await _userContextService.GetCurrentUserSub();
        DeleteUrlByIdResponseDto dto = await _urlService.DeleteUrlById(urlId, userId);

        return Ok(dto);
    }
}