namespace ShortURL.Controllers;
using Microsoft.AspNetCore.Mvc;
using ShortURL.DTOs.Auth;
using ShortURL.Services;
using ShortURL.Models;

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
        bool isValidLogin = await _userService.LoginAsync(login);

        if (!isValidLogin)
        {
            return Unauthorized("Invalid email or password.");
        }

        return Ok(new { message = "Login successful!" });
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto register)
    {
        User createdUser = await _userService.CreateUserAsync(register);

        return CreatedAtAction(nameof(Register), new { id = createdUser.Id }, createdUser);
    }
}