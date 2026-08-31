namespace ShortURL.DTOs;

using ShortURL.Enums;

public class LoginResponseDto
{
    public LoginResponseDto(Guid id, string email, UserTier tier)
    {
        Id = id;
        Email = email;
        Tier = tier;
    }

    public Guid Id { get; set; }

    public string Email { get; set; }

    public UserTier Tier { get; set; }

}