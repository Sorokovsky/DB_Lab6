using DB_Lab6.Configs;
using DB_Lab6.Database.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DB_Lab6.Database;

public class DatabaseContext
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly MongoConfig _config;
    
    public DatabaseContext(IOptions<MongoConfig> options)
    {
        _config = options.Value;
        var settings = MongoClientSettings.FromConnectionString(options.Value.ConnectionString);
        _client = new MongoClient(settings);
        _database = _client.GetDatabase(_config.DatabaseName);
    }
    
    public IMongoCollection<User> Users => _database.GetCollection<User>(nameof(Users));
    public IMongoCollection<Post> Posts => _database.GetCollection<Post>(nameof(Posts));
    public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>(nameof(Comments));
    
    public IMongoCollection<Followers> Followers => _database.GetCollection<Followers>(nameof(Followers));
    
    public IMongoClient Client => _client;
    public IMongoDatabase Database => _database;
}