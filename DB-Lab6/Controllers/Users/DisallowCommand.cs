using DB_Lab6.Console.Commands;
using DB_Lab6.Database.Repositories;
using DB_Lab6.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Users;

public class DisallowCommand : Command
{
    protected override string Name => "Відписатися";
    
    public override void Execute(CommandContext context)
    {
        var security = context.ServiceProvider.GetRequiredService<AuthorizationService>();
        var user = security.GetAuthorizedUser();
        if (user == null)
        {
            System.Console.WriteLine("Користувач не авторизований.");
            return;
        }
        var followersRepository = context.ServiceProvider.GetRequiredService<FollowersRepository>();
        var followers = followersRepository.GetByFollowerId(user.Id).Result.ToList();
        if (!followers.Any())
        {
            System.Console.WriteLine("Підписок не має.");
            return;
        }
        var usersRepository = context.ServiceProvider.GetRequiredService<UsersRepository>();
        System.Console.Write("Введіть електронну адресу користувача від якого відписатися: ");
        var email = System.Console.ReadLine() ?? string.Empty;
        var followed = usersRepository.GetUserByEmail(email).Result;
        if (followed == null)
        {
            System.Console.WriteLine("Користувача не існує");
            return;
        }
        var follower = followers.FirstOrDefault(x => x.FolloweredId == followed.Id);
        if (follower == null)
        {
            System.Console.WriteLine("Підписки не існує.");
            return;
        }

        followersRepository.DeleteAsync(follower).Wait();
        System.Console.WriteLine("Підписку успішно видалено.");
    }
}