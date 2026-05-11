using DB_Lab6.Console.Commands;
using DB_Lab6.Database;
using DB_Lab6.Database.Repositories;
using DB_Lab6.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Comments;

public class RemoveCommentCommand : Command
{
    protected override string Name => "Видалити коментар";
    public override void Execute(CommandContext context)
    {
        var security = context.ServiceProvider.GetRequiredService<AuthorizationService>();
        var user = security.GetAuthorizedUser();
        if (user == null)
        {
            System.Console.WriteLine("Користувач не авторизований");
            return;
        }
        var commentsRepository = context.ServiceProvider.GetRequiredService<CommentsRepository>();
        System.Console.Write("Введіть ідентифікатор: ");
        var commentId = System.Console.ReadLine() ?? string.Empty;
        var comment = commentsRepository.GetByIdAsync(commentId).Result;
        if (comment == null)
        {
            System.Console.WriteLine("Коментар не знайдено.");
            return;
        }

        if (comment.UserId != user.Id)
        {
            System.Console.WriteLine("Ви не є автором коментаря.");
            return;
        }
        var postsRepository = context.ServiceProvider.GetRequiredService<PostsRepository>();
        var post = postsRepository.GetByIdAsync(comment.PostId).Result;
        var database = context.ServiceProvider.GetRequiredService<DatabaseContext>();
        var session = database.Client.StartSession();
        try
        {
            session.StartTransaction();
            commentsRepository.DeleteAsync(session, comment).Wait();
            post.CommentsCount--;
            postsRepository.UpdateAsync(session, post).Wait();
        } catch(Exception exception)
        {
            System.Console.WriteLine($"Виникла помилка: {exception.Message}");
            session.AbortTransaction();
        }
    }
}