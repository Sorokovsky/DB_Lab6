using DB_Lab6.Database.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DB_Lab6.Database.Repositories;

public abstract class Repository<T> where T : BaseEntity
{
    private readonly DatabaseContext _context;
    protected readonly IMongoCollection<T> Collection;

    protected Repository(DatabaseContext context)
    {
        _context = context;
        Collection = GetCollection(context);
    }

    public async Task<T> SaveAsync(T entity)
    {
        if (entity.Id == ObjectId.Empty)
        {
            entity.Id = ObjectId.GenerateNewId();
        }
        await Collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task SaveManyAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.Id == ObjectId.Empty)
            {
                entity.Id = ObjectId.GenerateNewId();
            }
        }
        await Collection.InsertManyAsync(entities);
    }

    public async Task UpdateAsync(T document)
    {
        var filter = Builders<T>.Filter.Eq("_id", document.Id);
        await Collection.ReplaceOneAsync(filter, document);
    }

    public async Task DeleteAsync(T document)
    {
        var filter = Builders<T>.Filter.Eq("_id", document.Id);
        await Collection.DeleteOneAsync(filter);
    }

    public async Task DeleteManyAsync(IEnumerable<ObjectId> ids)
    {
        var filter = Builders<T>.Filter.In("_id", ids);
        await Collection.DeleteManyAsync(filter);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Collection.Find(_ => true).ToListAsync();
    }

    public async Task<T> GetByIdAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
        {
            return null;
        }
        else
        {
            return await GetByIdAsync(objectId);
        }
    }
    
    protected abstract IMongoCollection<T> GetCollection(DatabaseContext context); 
}