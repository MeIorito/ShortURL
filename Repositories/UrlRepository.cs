namespace ShortURL.Repositories;

using Microsoft.Extensions.Options;
using MongoDB.Bson;
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

    public async Task<List<Url>> GetAllUrls()
        {
            var filter = Builders<Url>.Filter.Empty;
            
            var cursor = await _urls.FindAsync(filter);
            return await cursor.ToListAsync();
        }
}