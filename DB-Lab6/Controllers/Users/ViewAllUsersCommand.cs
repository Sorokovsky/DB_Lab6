using DB_Lab6.Console.Commands;
using DB_Lab6.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Users;

public class ViewAllUsersCommand : Command
{
    protected override string Name => "Подивитися всіх користувачів";
    
    public override void Execute(CommandContext context)
    {
        var repository = context.ServiceProvider.GetRequiredService<UsersRepository>();
        var users = repository.GetAllAsync().Result;
        System.Console.WriteLine("Користувачі: ");
        foreach (var user in users)
        {
            System.Console.WriteLine(user);
        }
    }
}