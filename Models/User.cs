using ShortURL.Enums;

namespace ShortURL.Models;

public class User
{
    private User() { }

    public User(string username, string fullname, string email, string passwordHash)
    {

        Id = Guid.NewGuid();
        Username = username;
        Fullname = fullname;
        Email = email;
        PasswordHash = passwordHash;
        Tier = UserTier.Free;
    }

    public Guid Id { get; set; }

    public required string Username { get; set; }

    public required string Fullname { get; set; }

    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public UserTier Tier { get; set; }
}
