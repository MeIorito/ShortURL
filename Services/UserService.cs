namespace ShortURL.Services;

using ShortURL.Repositories;
using ShortURL.DTOs.Auth;
using ShortURL.Models;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(RegisterDto dto)
    {
        User user = new User
        (
            dto.Username,
            dto.FullName,
            dto.Email,
            dto.Password
        );

        return await _userRepository.CreateUser(user);
    }
}
