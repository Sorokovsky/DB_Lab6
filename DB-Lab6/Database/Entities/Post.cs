using MongoDB.Bson;

namespace DB_Lab6.Database.Entities;

public class Post : BaseEntity
{
    public Post(ObjectId id, string userId, string content, DateTime timestamps, int likes, int commentsCount) : base(id)
    {
        UserId = userId;
        Content = content;
        Timestamps = timestamps;
        Likes = likes;
        CommentsCount = commentsCount;
    }

    public string UserId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamps { get; set; }
    public int Likes { get; set; }
    public int CommentsCount { get; set; }
}