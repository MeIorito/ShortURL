namespace ShortURL.Controllers;
using Microsoft.AspNetCore.Mvc;
using ShortURL.DTOs.Auth;
using ShortURL.Services;
using FluentValidation;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{

    private readonly UserService _userService;
    private readonly IValidator<RegisterDto> _registerRequestValidator;


    public AuthController(
        UserService userService, 
        IValidator<RegisterDto> registerReguestValidator
        )
    {
        _userService = userService;
        _registerRequestValidator = registerReguestValidator;
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
        await _registerRequestValidator.ValidateAndThrowAsync(register);

        RegisterResponseDto dto = await _userService.CreateUserAsync(register);

        return CreatedAtAction(nameof(Register), new { id = dto.Id }, dto);
    }
}