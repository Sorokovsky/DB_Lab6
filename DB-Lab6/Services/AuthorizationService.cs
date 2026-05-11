using DB_Lab6.Database.Entities;
using DB_Lab6.Database.Repositories;

namespace DB_Lab6.Services;

public class AuthorizationService
{
    private readonly UsersRepository _repository;
    
    private User? _authorizedUser;

    public AuthorizationService(UsersRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> Register(User user)
    {
        var created = await _repository.SaveAsync(user);
        _authorizedUser = created;
        return created;
    }

    public async Task Login(string email, string password)
    {
        var user = await _repository.GetUserByEmail(email);
        if (user == null)
        {
            System.Console.WriteLine("Користувача не знайдено.");
            return;
        }

        if (user.Password.Equals(password))
        {
            _authorizedUser = user;
            System.Console.WriteLine("Успішна авторизація.");
        }
        else
        {
            System.Console.WriteLine("Пароль не правильний");
        }
    }

    public void Logout()
    {
        _authorizedUser = null;
    }
    
    public User? GetAuthorizedUser()
    {
        return _authorizedUser;
    }
}