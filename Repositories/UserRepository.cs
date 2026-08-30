namespace ShortURL.Repositories;

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

    public async Task<User> CreateUser(User user)
    {
        await _users.InsertOneAsync(user);
        
        return user;
    }

    // TODO Unique Email Index for faster checks
    public async Task<bool> IsEmailInUse(string email)
    {
        return await _users.Find(user => user.Email == email).AnyAsync();
    }

}