using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShortURL.Models;

public class Url
{
    private Url() { }

    [SetsRequiredMembers]
    public Url(string shortCode, string originalUrl, Guid userId, DateTime expiresAt)
    {
        ShortCode = shortCode;
        OriginalUrl = originalUrl;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;
        Clicks = 0;
    }

    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }

    public required string ShortCode { get; set; }

    public required string OriginalUrl { get; set; }

    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid UserId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime ExpiresAt { get; set; }

    public int Clicks { get; private set; }
}