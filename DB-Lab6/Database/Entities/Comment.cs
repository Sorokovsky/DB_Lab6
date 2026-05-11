// Entities/Comment.cs

using DB_Lab6.Database.Entities;
using MongoDB.Bson;

public class Comment : BaseEntity
{
    public Comment(ObjectId id, ObjectId postId, ObjectId userId, string content, DateTime timestamp) : base(id)
    {
        PostId = postId;
        UserId = userId;
        Content = content;
        Timestamp = timestamp;
    }

    public ObjectId PostId { get; set; }
    public ObjectId UserId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }

    public static Comment Enter(Post post, User user)
    {
        Console.Write("Введіть текст коментаря: ");
        var text = Console.ReadLine() ?? string.Empty;
        return new Comment(ObjectId.Empty, post.Id, user.Id, text, DateTime.UtcNow);
    }
}