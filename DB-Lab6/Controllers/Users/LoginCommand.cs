using DB_Lab6.Console.Commands;
using DB_Lab6.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Users;

public class LoginCommand : Command
{
    protected override string Name => "Увійти";
    public override void Execute(CommandContext context)
    {
        System.Console.Write("Введіть електронну адресу: ");
        var name = System.Console.ReadLine() ?? string.Empty;
        System.Console.Write("Введіть пароль: ");
        var password = System.Console.ReadLine() ?? string.Empty;
        var service = context.ServiceProvider.GetRequiredService<AuthorizationService>();
        service.Login(name, password).Wait();
    }
}