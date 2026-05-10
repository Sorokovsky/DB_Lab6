// Entities/Comment.cs

using DB_Lab6.Database.Entities;
using MongoDB.Bson;

public class Comment : BaseEntity
{
    public ObjectId PostId { get; set; }
    public ObjectId UserId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}