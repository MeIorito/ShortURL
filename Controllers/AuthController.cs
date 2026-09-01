namespace ShortURL.Controllers;
using Microsoft.AspNetCore.Mvc;
using ShortURL.DTOs.Auth;
using ShortURL.Services;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{

    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Login(LoginDto login)
    {
        LoginResponseDto dto = await _userService.LoginAsync(login);

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto register)
    {
        RegisterResponseDto dto = await _userService.CreateUserAsync(register);

        return CreatedAtAction(nameof(Register), new { id = dto.Id }, dto);
    }
}