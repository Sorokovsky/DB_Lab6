using DB_Lab6.Database.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DB_Lab6.Database.Repositories;

public class PostsRepository : Repository<Post>
{
    public PostsRepository(DatabaseContext context) : base(context)
    {
    }

    protected override IMongoCollection<Post> GetCollection(DatabaseContext context)
    {
        return context.Posts;
    }

    public async Task<IEnumerable<Post>> GetAllByUser(ObjectId userId)
    {
        var filter = Builders<Post>.Filter.Eq("UserId", userId);
        return await (await Collection.FindAsync(filter)).ToListAsync();
    }
}