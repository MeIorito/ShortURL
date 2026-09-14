using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShortURL.Models;

public class Click
{
    private Click() { }

    public Click(
        Guid urlId,
        string ipAddress,
        string? userAgent,
        string? referer
        )
    {
        UrlId = urlId;
        TimeStamp = DateTime.UtcNow;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        Referer = referer;
        ExpiresAt = TimeStamp.Add(TimeSpan.FromDays(365));
    }

    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; private set; }

    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid UrlId { get; private set; }

    public DateTime TimeStamp { get; private set; }

    public string IpAddress { get; private set; } = string.Empty;

    public string? UserAgent { get; private set; }

    public string? Referer { get; private set; }

    public DateTime ExpiresAt { get; private set; }
}