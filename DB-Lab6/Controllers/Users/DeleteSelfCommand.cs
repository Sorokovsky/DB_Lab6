using DB_Lab6.Console.Commands;
using DB_Lab6.Database;
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
        var usersRepository = context.ServiceProvider.GetRequiredService<UsersRepository>();
        var postsRepository = context.ServiceProvider.GetRequiredService<PostsRepository>();
        var commentsRepository = context.ServiceProvider.GetRequiredService<CommentsRepository>();
        var user = service.GetAuthorizedUser();
        if (user == null)
        {
            System.Console.WriteLine("Не авторизовано.");
            return;
        }
        var database = context.ServiceProvider.GetRequiredService<DatabaseContext>();
        var session = database.Client.StartSession();
        try
        {
            session.StartTransaction();
            usersRepository.DeleteAsync(session, user).Wait();
            var posts = postsRepository.GetAllByUser(user.Id).Result.Select(x => x.Id);
            postsRepository.DeleteManyAsync(session, posts).Wait();
            var comments = commentsRepository.GetByUser(user.Id).Result.Select(x => x.Id);
            commentsRepository.DeleteManyAsync(session, comments).Wait();
            var followersRepository = context.ServiceProvider.GetRequiredService<FollowersRepository>();
            followersRepository.RemoveByUserAsync(session, user.Id).Wait();
            service.Logout();
            System.Console.WriteLine("Успішно видалено.");
            session.CommitTransaction();
        }
        catch (Exception exception)
        {
            session.AbortTransaction();
            System.Console.WriteLine("Сталася помилка: " + exception.Message);
        }
    }
}