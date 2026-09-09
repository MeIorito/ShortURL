namespace ShortURL.Services;

using ShortURL.Repositories;
using ShortURL.Models;
using ShortURL.DTOs;
using System;
using ShortURL.Enums;
using ShortURL.Exceptions;

public class UrlService
{
    private readonly UrlRepository _urlRepository;
    private static Random random = new Random();

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
            RandomString(6),
            dto.url,
            userId,
            DateTime.UtcNow.Add(timeAlive)
        );

        return new CreateUrlResponseDto(await _urlRepository.CreateUrl(url));
    }

    public async Task<string> GetOriginalUrl(string code)
    {
        string? originalUrl = await _urlRepository.GetOriginalUrl(code) ?? throw new UrlNotFoundException();

        return originalUrl;
    }

    public async Task<GetAllUrlsResponseDto> GetAllUrlsAsync()
    {
        var urls = await _urlRepository.GetAllUrls();

        return new GetAllUrlsResponseDto(urls);
    }

    public async Task<GetAllUrlsResponseDto> GetAllUrlsByUserId(Guid userId)
    {
        var urls = await _urlRepository.GetAllUrlsByUserId(userId);

        return new GetAllUrlsResponseDto(urls);
    }

    /*TODO Both situations return the same exception while the reason differs
      Create different exceptions.
    */ 
    public async Task<DeleteUrlByIdResponseDto> DeleteUrlById(Guid urlId, Guid userId)
    {
        var deleteResult =  await _urlRepository.DeleteUrlByIdWithUserId(urlId, userId);

        if (deleteResult.DeletedCount == 0)
        {
            throw new UrlNotFoundException();
        }

        return new DeleteUrlByIdResponseDto(deleteResult.IsAcknowledged, deleteResult.DeletedCount);
    }

    // Helper functions
    private static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}