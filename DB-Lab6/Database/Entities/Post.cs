using MongoDB.Bson;

namespace DB_Lab6.Database.Entities;

public class Post : BaseEntity
{
    public Post(ObjectId id, ObjectId userId, string content, DateTime timestamps, int likes, int commentsCount) : base(id)
    {
        UserId = userId;
        Content = content;
        Timestamps = timestamps;
        Likes = likes;
        CommentsCount = commentsCount;
    }

    public ObjectId UserId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamps { get; set; }
    public int Likes { get; set; }
    public int CommentsCount { get; set; }

    public override string ToString()
    {
        var result = string.Empty;
        result += $"Ідентифікатор: {Id}\n";
        result += $"Зміст: {Content}\n";
        result += $"Кількість вподобайок: {Likes}\n";
        result += $"Кількість коментарів: {CommentsCount}\n";
        result += $"Ідентифікатор автора: {UserId}\n";
        result += $"Час створення: {Timestamps}";
        return result;
    }

    public static Post Enter(User user)
    {
        System.Console.Write("Введіть текст публікації: ");
        var content = System.Console.ReadLine() ?? string.Empty;
        return new Post(ObjectId.Empty, user.Id, content, DateTime.UtcNow, 0, 0);
    }
}