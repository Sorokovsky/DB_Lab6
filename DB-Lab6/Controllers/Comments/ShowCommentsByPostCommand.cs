using DB_Lab6.Console.Commands;
using DB_Lab6.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Comments;

public class ShowCommentsByPostCommand : Command
{
    protected override string Name => "Показати коментарі до публікації";
    public override void Execute(CommandContext context)
    {
        var commentsRepository = context.ServiceProvider.GetRequiredService<CommentsRepository>();
        var postsRepository = context.ServiceProvider.GetRequiredService<PostsRepository>();
        System.Console.Write("Введіть ідентифікатор публікації: ");
        var postId = System.Console.ReadLine() ?? string.Empty;
        var post = postsRepository.GetByIdAsync(postId).Result;
        if (post == null)
        {
            System.Console.WriteLine("Публікацію не знайдено.");
            return;
        }
        var comments = commentsRepository.GetByPost(post.Id).Result;
        System.Console.WriteLine("Коментарі");
        foreach (var comment in comments)
        {
            System.Console.WriteLine(comment);
        }
    }
}