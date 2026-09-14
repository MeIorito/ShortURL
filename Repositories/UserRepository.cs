namespace ShortURL.Repositories;

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShortURL.Configuration;
using ShortURL.Models;

public class UserRepository
{
    private readonly IMongoCollection<User> _users;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(
        IMongoClient mongoClient,
        IOptions<MongoDbSettings> settings,
        ILogger<UserRepository> logger
        )
    {
        var database = mongoClient.GetDatabase(
            settings.Value.DatabaseName);

        _users = database.GetCollection<User>("users");
        _logger = logger;
    }

    public async Task<User> CreateUser(User user)
    {
        _logger.LogInformation("Creating a new user with email: {Email}", user.Email);

        await _users.InsertOneAsync(user);
        
        _logger.LogInformation("Successfully created a new user with email: {Email}, and userId: {UserId}", user.Email, user.Id);
        return user;
    }

    public async Task<User?> GetUser(string email)
    {
        _logger.LogInformation("Fetching user from database with email: {Email}", email);

        return await _users.Find(user => user.Email == email).FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        _logger.LogInformation(
            "Fetching user with ID: {UserId}",
            userId);

        return await _users
            .Find(user => user.Id == userId)
            .FirstOrDefaultAsync();
    }

    // TODO Unique Email Index for faster checks
    public async Task<bool> IsEmailInUse(string email)
    {
        _logger.LogInformation("Checking if email: {Email} is in use", email);

        return await _users.Find(user => user.Email == email).AnyAsync();
    }

    public async Task<List<User>> GetAllUsers()
    {
        _logger.LogInformation(
            "Fetching all users from the database.");

        var users = await _users
            .Find(Builders<User>.Filter.Empty)
            .ToListAsync();

        _logger.LogInformation(
            "Successfully retrieved {Count} users.",
            users.Count);

        return users;
    }

    public async Task<bool> UpdateUser(User user)
    {
        _logger.LogInformation(
            "Updating user with ID: {UserId}",
            user.Id);

        var result = await _users.ReplaceOneAsync(
            u => u.Id == user.Id,
            user);

        _logger.LogInformation(
            "User update completed. Modified count: {ModifiedCount}",
            result.ModifiedCount);

        return result.ModifiedCount > 0;
    }

    public async Task<DeleteResult> DeleteUser(Guid userId)
    {
        _logger.LogInformation(
            "Deleting user with ID: {UserId}",
            userId);

        var result = await _users.DeleteOneAsync(
            user => user.Id == userId);

        _logger.LogInformation(
            "User deletion completed. Deleted count: {DeletedCount}",
            result.DeletedCount);

        return result;
    }
}