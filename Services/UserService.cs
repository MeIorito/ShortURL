namespace ShortURL.Services;

using ShortURL.Repositories;
using ShortURL.DTOs.Auth;
using ShortURL.Models;
using ShortURL.Exceptions;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<RegisterResponseDto> CreateUserAsync(RegisterDto dto)
    {
        bool EmailInUse = await _userRepository.IsEmailInUse(dto.Email);

        if (EmailInUse)
        {
            throw new EmailAlreadyExistsException();
        }

        User user = new User
        (
            dto.Username,
            dto.FullName,
            dto.Email,
            BCrypt.Net.BCrypt.EnhancedHashPassword(dto.Password, workFactor: 13)
        );

        return new RegisterResponseDto(await _userRepository.CreateUser(user));
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        User? user = await _userRepository.GetUser(dto.Email);

        if (user == null)
        {
            BCrypt.Net.BCrypt.EnhancedVerify("fakehashforenum", "$2a$13$yvPLqfbRGUFiRqReHsDXIefu0gJGSUOa6QVMkLNRs2sZtBlDPm8rq");
            throw new UserNotFoundException();
        }

        if (!BCrypt.Net.BCrypt.EnhancedVerify(dto.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }

        return new LoginResponseDto(user);
    }
}
