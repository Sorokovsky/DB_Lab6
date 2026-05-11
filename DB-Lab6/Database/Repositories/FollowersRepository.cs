using DB_Lab6.Database.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DB_Lab6.Database.Repositories;

public class FollowersRepository : Repository<Followers>
{
    public FollowersRepository(DatabaseContext context) : base(context)
    {
    }

    protected override IMongoCollection<Followers> GetCollection(DatabaseContext context)
    {
        return context.Followers;
    }

    public async Task RemoveByUserAsync(IClientSessionHandle session, ObjectId userId)
    {
        var filter = Builders<Followers>.Filter.Or(
            Builders<Followers>.Filter.Eq("FolloweredId", userId),
            Builders<Followers>.Filter.Eq("FollowerId", userId));
        await Collection.DeleteManyAsync(filter);
    }
}