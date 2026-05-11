using DB_Lab6.Console.Commands;
using DB_Lab6.Database;
using DB_Lab6.Database.Repositories;
using DB_Lab6.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Comments;

public class CreateCommentCommand : Command
{
    protected override string Name => "Створити коментар";
    
    public override void Execute(CommandContext context)
    {
        var security = context.ServiceProvider.GetRequiredService<AuthorizationService>();
        var user = security.GetAuthorizedUser();
        if (user == null)
        {
            System.Console.WriteLine("Користувач не авторизований");
            return;
        }

        var postsRepository = context.ServiceProvider.GetRequiredService<PostsRepository>();
        System.Console.Write("Введіть ідентифікатор публікації: ");
        var postId = System.Console.ReadLine() ?? string.Empty;
        var post = postsRepository.GetByIdAsync(postId).Result;
        if (post == null)
        {
            System.Console.WriteLine("Публікацію не знайдено.");
            return;
        }
        var comment = Comment.Enter(post, user);
        var database = context.ServiceProvider.GetRequiredService<DatabaseContext>();
        var commentRepository = context.ServiceProvider.GetRequiredService<CommentsRepository>();
        using var session = database.Client.StartSession();
        session.StartTransaction();
        try
        {
            commentRepository.SaveAsync(comment).Wait();
            post.CommentsCount++;
            postsRepository.UpdateAsync(post).Wait();
            session.CommitTransaction();
            System.Console.WriteLine("Коментар успішно створено.");
        }
        catch (Exception exception)
        {
            session.AbortTransaction();
            System.Console.WriteLine($"Сталася невідома помилка({exception.Message}), повторіть дії.");
        }
    }
}