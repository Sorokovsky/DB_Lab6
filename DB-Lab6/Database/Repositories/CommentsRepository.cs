using MongoDB.Bson;
using MongoDB.Driver;

namespace DB_Lab6.Database.Repositories;

public class CommentsRepository : Repository<Comment>
{
    public CommentsRepository(DatabaseContext context) : base(context)
    {
    }

    protected override IMongoCollection<Comment> GetCollection(DatabaseContext context)
    {
        return context.Comments;
    }

    public async Task<IEnumerable<Comment>> GetByUser(ObjectId userId)
    {
        var filter = Builders<Comment>.Filter.Eq("UserId", userId);
        return await (await Collection.FindAsync(filter)).ToListAsync();
    }
    
    public async Task<IEnumerable<Comment>> GetByPost(ObjectId postId)
    {
        var filter = Builders<Comment>.Filter.Eq("PostId", postId);
        return await (await Collection.FindAsync(filter)).ToListAsync();
    }
}