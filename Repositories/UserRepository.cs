using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShortURL.Configuration;
using ShortURL.Models;


public class UserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(
        IMongoClient mongoClient,
        IOptions<MongoDbSettings> settings)
    {
        var database = mongoClient.GetDatabase(
            settings.Value.DatabaseName);

        _users = database.GetCollection<User>("users");
    }
}