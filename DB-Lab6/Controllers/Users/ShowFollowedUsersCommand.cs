using DB_Lab6.Console.Commands;
using DB_Lab6.Database.Repositories;
using DB_Lab6.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Users;

public class ShowFollowedUsersCommand : Command
{
    protected override string Name => "Подивитися підписки";
    public override void Execute(CommandContext context)
    {
        var security = context.ServiceProvider.GetRequiredService<AuthorizationService>();
        var user = security.GetAuthorizedUser();
        if (user == null)
        {
            System.Console.WriteLine("Користувача не знайдено.");
            return;
        }

        var followersRepository = context.ServiceProvider.GetRequiredService<FollowersRepository>();
        var followers = followersRepository.GetByFollowerId(user.Id).Result.Select(x => x.FolloweredId);
        var usersRepository = context.ServiceProvider.GetRequiredService<UsersRepository>();
        var users = usersRepository.GetManyByIdsAsync(followers).Result;
        System.Console.WriteLine("Підписки");
        foreach (var follower in users)
        {
            System.Console.WriteLine(follower);
        }
    }
}