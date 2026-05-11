using DB_Lab6.Console.Commands;
using DB_Lab6.Database.Repositories;
using DB_Lab6.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DB_Lab6.Controllers.Users;

public class DeleteSelfCommand : Command
{
    protected override string Name => "Видалити себе";
    
    public override void Execute(CommandContext context)
    {
        var service = context.ServiceProvider.GetRequiredService<AuthorizationService>();
        var repository = context.ServiceProvider.GetRequiredService<UsersRepository>();
        var user = service.GetAuthorizedUser();
        if (user == null)
        {
            System.Console.WriteLine("Не авторизовано.");
        }
        else
        {
            repository.DeleteAsync(user).Wait();
            service.Logout();
            System.Console.WriteLine("Успішно видалено.");
        }
    }
}