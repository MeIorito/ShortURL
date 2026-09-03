namespace ShortURL.Services;

using ShortURL.Repositories;
using ShortURL.DTOs.Auth;
using ShortURL.Models;
using ShortURL.Exceptions;
using ShortURL.DTOs;
using System;

public class UrlService
{
    private readonly UrlRepository _urlRepository;

    public UrlService(UrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }

    // All dynamic stuff still hardcoded
    public async Task<CreateUrlResponseDto> CreateUrlAsync(CreateUrlDto dto)
    {
        Url url = new Url(
            "XXxxXX",
            dto.url,
            Guid.NewGuid(),
            DateTime.UtcNow.Add(new TimeSpan(1,0,0))
        );

        return new CreateUrlResponseDto(await _urlRepository.CreateUrl(url));
    }
}