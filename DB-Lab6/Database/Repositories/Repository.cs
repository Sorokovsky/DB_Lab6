using DB_Lab6.Database.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DB_Lab6.Database.Repositories;

public class Repository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection;

    public Repository(MongoClient client, string databaseName, string collectionName)
    {
        _collection = client.GetDatabase(databaseName).GetCollection<T>(collectionName);
    }

    public async Task SaveAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task Update(T document)
    {
       var filter = Builders<T>.Filter.Eq("_id", document.Id);
       await _collection.ReplaceOneAsync(filter, document);
    }

    public async Task Delete(T document)
    {
        await _collection.DeleteOneAsync(new BsonDocument("_id", document.Id));
    }

    public async Task<T> GetById(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}