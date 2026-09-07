using ShortURL.Models;

namespace ShortURL.DTOs;

public class GetAllUrlsResponseDto
{
    public GetAllUrlsResponseDto(List<Url> urls)
    {
        Urls = urls;
    }

    public List<Url> Urls { get; set;}
}