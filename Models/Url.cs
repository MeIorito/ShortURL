namespace ShortURL.Models;

public class Url
{
    private Url() { }

    public Url(string shortCode, string originalUrl, Guid userId, DateTime expiresAt)
    {
        Id = Guid.NewGuid();
        ShortCode = shortCode;
        OriginalUrl = originalUrl;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;
        Clicks = 0;
    }

    public Guid Id { get; private set; }

    public required string ShortCode { get; set; }

    public required string OriginalUrl { get; set; }

    public Guid UserId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime ExpiresAt { get; set; }

    public int Clicks { get; private set; }
}