using DB_Lab6.Console.Commands;
using DB_Lab6.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Users;

public class ProfileCommand : Command
{
    protected override string Name => "Авторизований користувач";
    public override void Execute(CommandContext context)
    {
        var service = context.ServiceProvider.GetRequiredService<AuthorizationService>();
        var user = service.GetAuthorizedUser();
        if (user == null)
        {
            System.Console.WriteLine("Не авторизовано.");
        }
        else
        {
            System.Console.WriteLine("Авторизований користувач.");
            System.Console.WriteLine(user);
        }
    }
}