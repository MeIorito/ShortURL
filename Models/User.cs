using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis; 

using ShortURL.Enums;

namespace ShortURL.Models;

public class User
{
    private User() { }

    [SetsRequiredMembers]
    public User(string username, string fullname, string email, string passwordHash)
    {

        Username = username;
        Fullname = fullname;
        Email = email;
        PasswordHash = passwordHash;
        Tier = UserTier.Free;
    }

    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }

    public required string Username { get; set; }

    public required string Fullname { get; set; }

    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public UserTier Tier { get; set; }
}
