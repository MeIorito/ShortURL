namespace ShortURL.DTOs.Auth;

using ShortURL.Enums;
using ShortURL.Models;

public class RegisterResponseDto
{
        public RegisterResponseDto(User user)
    {
        Id = user.Id;
        Email = user.Email;
        Tier = user.Tier;
    }

    public Guid Id { get; set; }

    public string Email { get; set; }

    public UserTier Tier { get; set; }
}