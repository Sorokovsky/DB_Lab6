using DB_Lab6.Console.Commands;
using DB_Lab6.Database.Entities;
using DB_Lab6.Database.Repositories;
using DB_Lab6.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;

namespace DB_Lab6.Controllers.Users;

public class FollowCommand : Command
{
    protected override string Name => "Спостерігати";
    
    public override void Execute(CommandContext context)
    {
        var security = context.ServiceProvider.GetRequiredService<AuthorizationService>();
        var user = security.GetAuthorizedUser();
        if (user == null)
        {
            System.Console.WriteLine("Користувач не авторизовано.");
            return;
        }
        var usersRepository = context.ServiceProvider.GetRequiredService<UsersRepository>();
        System.Console.Write("Введіть електронну адресу користувача за яким хочете стежити: ");
        var email = System.Console.ReadLine() ?? string.Empty;
        var followed = usersRepository.GetUserByEmail(email).Result;
        if (followed == null)
        {
            System.Console.WriteLine("Користувача не знайдено.");
            return;
        }
        var followersRepository = context.ServiceProvider.GetRequiredService<FollowersRepository>();
        var followers = new Followers(ObjectId.Empty, user.Id, followed.Id);
        followersRepository.SaveAsync(followers).Wait();
        System.Console.WriteLine($"Стеження за {followed.Name} оформлено.");
    }
}