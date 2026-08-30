namespace ShortURL.Controllers;
using Microsoft.AspNetCore.Mvc;
using ShortURL.DTOs.Auth;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    [HttpGet]
    public IActionResult Login(LoginDto login)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Register(RegisterDto register)
    {
        return Ok();
    }
}