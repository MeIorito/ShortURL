using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShortURL.Configuration;
using ShortURL.Services;
using ShortURL.Repositories;
using ShortURL.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp
        .GetRequiredService<IOptions<MongoDbSettings>>()
        .Value;

    return new MongoClient(settings.ConnectionString);
});

// Add global exceptionn handler and enable problem details
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Register repo's and services for DI
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
