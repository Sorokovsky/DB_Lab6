using MongoDB.Bson;

namespace DB_Lab6.Database.Entities;

public class Followers : BaseEntity
{
    public Followers(ObjectId id, ObjectId followerId, ObjectId followeredId) : base(id)
    {
        FollowerId = followerId;
        FolloweredId = followeredId;
    }
    
    public ObjectId FollowerId { get; set; }
    public ObjectId FolloweredId { get; set; }
}