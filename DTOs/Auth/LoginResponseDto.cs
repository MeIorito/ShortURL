namespace ShortURL.DTOs.Auth;

using ShortURL.Enums;
using ShortURL.Models;

public class LoginResponseDto
{
    public LoginResponseDto(User user, string jwt)
    {
        Id = user.Id;
        Email = user.Email;
        Tier = user.Tier;
        Jwt = jwt;
    }

    public Guid Id { get; set; }

    public string Email { get; set; }

    public UserTier Tier { get; set; }

    public string Jwt { get; }

}