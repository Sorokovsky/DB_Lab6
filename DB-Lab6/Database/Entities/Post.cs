namespace DB_Lab6.Database.Entities;

public class Post : BaseEntity
{
    public string UserId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamps { get; set; }
    public int Likes { get; set; }
    public int CommentsCount { get; set; }
}