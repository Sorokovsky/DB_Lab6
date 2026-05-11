using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DB_Lab6.Database.Entities;

public abstract class BaseEntity
{
    [BsonId]
    public ObjectId Id { get; set; }

    protected BaseEntity(ObjectId id)
    {
        Id = id;
    }
}