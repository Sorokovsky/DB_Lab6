using DB_Lab6.Console.Commands;
using DB_Lab6.Database.Repositories;
using DB_Lab6.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Posts;

public class UpdatePostCommand : Command
{
    protected override string Name => "Оновити публікацію";
    public override void Execute(CommandContext context)
    {
        var security = context.ServiceProvider.GetRequiredService<AuthorizationService>();
        var user = security.GetAuthorizedUser();
        if (user == null)
        {
            System.Console.WriteLine("Користувач не авторизований.");
        }
        else
        {
            var repository = context.ServiceProvider.GetRequiredService<PostsRepository>();
            System.Console.Write("Введіть ідентифікатор публікації: ");
            var postId = System.Console.ReadLine() ?? string.Empty;
            var post = repository.GetByIdAsync(postId).Result;
            if (post == null)
            {
                System.Console.WriteLine("Публікація не знайдена.");
            }
            else
            {
                if (post.UserId != user.Id)
                {
                    System.Console.WriteLine("Ви не власник публікації");
                    return;
                }
                System.Console.Write("Введіть новий текст: ");
                var newText = System.Console.ReadLine() ?? string.Empty;
                post.Content = newText;
                repository.UpdateAsync(post).Wait();
                System.Console.WriteLine("Успішно оновлено.");
            }
        }
    }
}