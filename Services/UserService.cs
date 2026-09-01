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

    public async Task<User> CreateUserAsync(RegisterDto dto)
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
            dto.Password
        );

        return await _userRepository.CreateUser(user);
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        User? user = await _userRepository.GetUser(dto.Email);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        if (dto.Password != user.PasswordHash)
        {
            throw new InvalidCredentialsException();
        }

        return new LoginResponseDto(
            user.Id,
            user.Email,
            user.Tier
        );
    }
}
