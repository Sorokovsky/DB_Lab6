using DB_Lab6.Database.Entities;
using MongoDB.Driver;

namespace DB_Lab6.Database.Repositories;

public class UsersRepository : Repository<User>
{
    public UsersRepository(DatabaseContext context) : base(context)
    {
    }

    protected override IMongoCollection<User> GetCollection(DatabaseContext context)
    {
        return context.Users;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var filter = Builders<User>.Filter.Eq("Email", email);
        return await (await Collection.FindAsync(filter)).FirstOrDefaultAsync();
    }
}