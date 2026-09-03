using ShortURL.Models;

namespace ShortURL.DTOs;

public class CreateUrlResponseDto
{

    public CreateUrlResponseDto(Url url)
    {
        Id = url.Id;
        ShortCode = url.ShortCode;
        OriginalUrl = url.OriginalUrl;
        UserId = url.UserId;
        ExpiresAt = url.ExpiresAt;
    }

    public Guid Id { get; private set; }

    public  string ShortCode { get; set; }

    public  string OriginalUrl { get; set; }

    public Guid UserId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime ExpiresAt { get; set; }
}