namespace ShortURL.Repositories;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShortURL.Configuration;
using ShortURL.Controllers;
using ShortURL.Models;

public class UrlRepository
{
    private readonly IMongoCollection<Url> _urls;
    private readonly ILogger<UrlRepository> _logger;

    public UrlRepository(
        IMongoClient mongoClient, 
        IOptions<MongoDbSettings> settings,
        ILogger<UrlRepository> logger
        )
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _urls = database.GetCollection<Url>("urls");
        _logger = logger;
    }

    public async Task<Url> CreateUrl(Url url)
    {
        _logger.LogInformation("Creating a new short URL. Destination: {DestinationUrl}", url.OriginalUrl);

        await _urls.InsertOneAsync(url);
        
        _logger.LogInformation("Successfully created short URL with ID: {UrlId} for User: {UserId}", url.Id, url.UserId);
        return url;
    }

    public async Task<string?> GetOriginalUrl(string code)
    {
        _logger.LogInformation("Fetching original URL for code {code}", code);

        var urlDocument = await _urls.Find(u => u.ShortCode == code).FirstOrDefaultAsync();

        _logger.LogInformation("Original url found is {url} ", urlDocument?.OriginalUrl);

        return urlDocument?.OriginalUrl;
    }

    public async Task<List<Url>> GetAllUrls()
    {
        _logger.LogInformation("Fetching all URLs from the database.");
        
        var filter = Builders<Url>.Filter.Empty;
        var cursor = await _urls.FindAsync(filter);
        var list = await cursor.ToListAsync();
        
        _logger.LogInformation("Successfully retrieved {Count} URLs.", list.Count);
        return list;
    }

    public async Task<List<Url>> GetAllUrlsByUserId(Guid userId)
    {
        _logger.LogInformation("Fetching all URLs from user with userId: {} from the database.", userId);

        var cursor = await _urls.FindAsync(u => u.UserId == userId);
        var list = await cursor.ToListAsync();

        _logger.LogInformation("Successfully retrieved {Count} URLs.", list.Count);
        return list;
    }
    
    public async Task<DeleteResult> DeleteUrlByIdWithUserId(Guid urlId, Guid userId)
    {
        _logger.LogInformation("Received request to delete URL ID: {UrlId} requested by User ID: {UserId}", urlId, userId);

        var existingUrl = await _urls.Find(u => u.Id == urlId)
                                 .Project(u => new { u.UserId })
                                 .FirstOrDefaultAsync();

        if (existingUrl == null)
        {
            _logger.LogWarning("Delete failed: URL ID: {UrlId} was not found.", urlId);
            throw new KeyNotFoundException("The URL ID does not exist.");
        }

        if (existingUrl.UserId != userId)
        {
            _logger.LogWarning("Unauthorized delete attempt! User ID: {UserId} tried to delete URL ID: {UrlId} owned by User ID: {OwnerId}", 
                userId, urlId, existingUrl.UserId);
            throw new UnauthorizedAccessException("You do not have permission to delete this URL.");
        }

        var result = await _urls.DeleteOneAsync(u => u.Id == urlId);
        
        _logger.LogInformation("Successfully deleted URL ID: {UrlId}. Deleted count: {DeletedCount}", urlId, result.DeletedCount);
        return result;
    }
}
