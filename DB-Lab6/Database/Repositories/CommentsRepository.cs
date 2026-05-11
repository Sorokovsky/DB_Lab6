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
}