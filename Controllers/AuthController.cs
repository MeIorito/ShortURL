namespace ShortURL.Controllers;
using Microsoft.AspNetCore.Mvc;
using ShortURL.DTOs.Auth;
using ShortURL.Services;
using ShortURL.Validators;
using FluentValidation;
using ShortURL.Exceptions;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{

    private readonly UserService _userService;
    private readonly IValidator<LoginDto> _loginRequestValidator;

    public AuthController(UserService userService, IValidator<LoginDto> loginRequestValidator)
    {
        _userService = userService;
        _loginRequestValidator = loginRequestValidator;
    }

    [HttpGet]
    public async Task<IActionResult> Login(LoginDto login)
    {
        await _loginRequestValidator.ValidateAndThrowAsync(login);

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