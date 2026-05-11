using DB_Lab6.Console.Commands;
using DB_Lab6.Database.Entities;
using DB_Lab6.Database.Repositories;
using DB_Lab6.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Posts;

public class CreatePostCommand : Command
{
    protected override string Name => "Створити публікацію";
    
    public override void Execute(CommandContext context)
    {
        var service = context.ServiceProvider.GetRequiredService<AuthorizationService>();
        var user = service.GetAuthorizedUser();
        if (user == null)
        {
            System.Console.WriteLine("Не авторизований користувач.");   
        }
        else
        {
            var repository = context.ServiceProvider.GetRequiredService<PostsRepository>();
            var post = Post.Enter(user);
            repository.SaveAsync(post).Wait();
            System.Console.WriteLine("Успішне створення.");
        }
    }
}