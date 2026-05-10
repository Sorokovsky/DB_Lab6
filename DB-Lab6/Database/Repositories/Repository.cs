using DB_Lab6.Database.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DB_Lab6.Database.Repositories;

public class Repository<T> where T : BsonValue
{
    private readonly IMongoCollection<T> _collection;

    public Repository(MongoClient client, string databaseName, string collectionName)
    {
        _collection = client.GetDatabase(databaseName).GetCollection<T>(collectionName);
    }

    public void Save(T entity)
    {
        _collection.InsertOne(entity);
    }

    public void Update(T document)
    {
        _collection.UpdateOne(new BsonDocument("_id", document.AsObjectId), new BsonDocument("$set", document));
    }

    public void Delete(T document)
    {
        _collection.DeleteOne(new BsonDocument("_id", document.AsObjectId));
    }

    public T GetById(string id)
    {
        return _collection.Find(new BsonDocument("_id", id)).FirstOrDefault();
    }
}