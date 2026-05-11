using DB_Lab6.Database.Entities;
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
}