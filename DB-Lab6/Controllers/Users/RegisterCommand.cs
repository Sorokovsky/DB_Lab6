using DB_Lab6.Console.Commands;
using DB_Lab6.Database.Entities;
using DB_Lab6.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Users;

public class RegisterCommand : Command
{
    protected override string Name => "Зареєструватися";
    public override void Execute(CommandContext context)
    {
        var service = context.ServiceProvider.GetRequiredService<AuthorizationService>();
        var user = User.Enter();
        service.Register(user).Wait();
    }
}