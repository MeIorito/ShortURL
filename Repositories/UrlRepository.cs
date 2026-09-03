namespace ShortURL.Repositories;

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShortURL.Configuration;
using ShortURL.Models;

public class UrlRepository
{
    private readonly IMongoCollection<Url> _urls;

    public UrlRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
    {
        var database = mongoClient.GetDatabase(
            settings.Value.DatabaseName);

        _urls = database.GetCollection<Url>("urls");
    }

    public async Task<Url> CreateUrl(Url url)
    {
        await _urls.InsertOneAsync(url);

        return url;
    }
}