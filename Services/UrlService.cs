namespace ShortURL.Services;

using ShortURL.Repositories;
using ShortURL.Models;
using ShortURL.DTOs;
using System;
using ShortURL.Enums;

public class UrlService
{
    private readonly UrlRepository _urlRepository;

    public UrlService(UrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }

    // All dynamic stuff still hardcoded
    public async Task<CreateUrlResponseDto> CreateUrlAsync(CreateUrlDto dto, Guid? userId, UserTier role)
    {

        TimeSpan timeAlive;

        switch(role)
        {
            case UserTier.Anonymous:
                timeAlive = TimeSpan.FromDays(1);
                break;
            case UserTier.Free:
                timeAlive = TimeSpan.FromDays(7);
                break;
            case UserTier.Paid:
                timeAlive = TimeSpan.MaxValue;
                break;
            default:
                throw new InvalidOperationException("User role is invalid.");
        }

        Url url = new Url(
            "XXxxXX",
            dto.url,
            userId,
            DateTime.UtcNow.Add(timeAlive)
        );

        return new CreateUrlResponseDto(await _urlRepository.CreateUrl(url));
    }

    public async Task<GetAllUrlsResponseDto> GetAllUrlsAsync()
    {
        var urls = await _urlRepository.GetAllUrls();

        return new GetAllUrlsResponseDto(urls);
    }
}