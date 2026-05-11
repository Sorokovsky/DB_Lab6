using DB_Lab6.Configs;
using DB_Lab6.Console.Commands;
using DB_Lab6.Controllers.Users;
using DB_Lab6.Database;
using DB_Lab6.Database.Repositories;
using DB_Lab6.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DB_Lab6;

public static class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        var services = new ServiceCollection();
        services.Configure<MongoConfig>(configuration.GetSection(nameof(MongoConfig)));
        services.AddSingleton<DatabaseContext>();
        services.AddSingleton<UsersRepository>();
        services.AddSingleton<PostsRepository>();
        services.AddSingleton<CommentsRepository>();
        services.AddSingleton<AuthorizationService>();
        services.AddSingleton<IMongoClient>(builder =>
        {
            var config = builder.GetRequiredService<IOptions<MongoConfig>>().Value;
            return new MongoClient(config.ConnectionString);
        });
        
        var serviceProvider = services.BuildServiceProvider();
        SetupCommands(serviceProvider);
    }

    private static void SetupCommands(IServiceProvider serviceProvider)
    {
        var context = new CommandContext("Головне меню", serviceProvider);
        context.AddCommand(new ExitCommand()); 
        context.SetupUserController();
        context.Start();
    }

    private static void SetupUserController(this CommandContext context)
    {
        var userContext = new CommandContext("Користувачі", context.ServiceProvider);
        userContext.AddCommand(new ExitCommand());
        userContext.AddCommand(new RegisterCommand());
        userContext.AddCommand(new LoginCommand());
        userContext.AddCommand(new ProfileCommand());
        userContext.AddCommand(new ViewAllUsersCommand());
        userContext.AddCommand(new DeleteSelfCommand());
        context.AddCommand(userContext);
    }
}