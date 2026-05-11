using DB_Lab6.Console.Commands;
using DB_Lab6.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Posts;

public class GetPostsOfUserCommand : Command
{
    protected override string Name => "Подивитися публікації користувача";
    
    public override void Execute(CommandContext context)
    {
        System.Console.Write("Електронна адреса користувача: ");
        var email = System.Console.ReadLine() ?? string.Empty;
        var usersRepository = context.ServiceProvider.GetRequiredService<UsersRepository>();
        var user = usersRepository.GetUserByEmail(email).Result;
        if (user == null)
        {
            System.Console.WriteLine("Користувача не знайдено.");
        }
        else
        {
            var postsRepository = context.ServiceProvider.GetRequiredService<PostsRepository>();
            var posts = postsRepository.GetAllByUser(user.Id).Result;
            System.Console.WriteLine($"Публікації користувача {user.Name}");
            foreach (var post in posts)
            {
                System.Console.WriteLine(post);
            }
        }
    }
}