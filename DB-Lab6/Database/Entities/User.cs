using MongoDB.Bson;

namespace DB_Lab6.Database.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfileDetails { get; set; }

    public User(ObjectId id, string name, string email, string password, string profileDetails) : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
        ProfileDetails = profileDetails;
    }

    public static User Enter()
    {
        System.Console.Write("Введіть ім'я користувача: ");
        var name = System.Console.ReadLine() ?? string.Empty;
        System.Console.Write("Введіть електронну адресу користувача: ");
        var email = System.Console.ReadLine() ?? string.Empty;
        System.Console.Write("Введіть пароль користувача: ");
        var password = System.Console.ReadLine() ?? string.Empty;
        System.Console.Write("Введіть данні користувача: ");
        var data = System.Console.ReadLine() ?? string.Empty;
        return new User(ObjectId.Empty, name, email, password, data);
    }

    public override string ToString()
    {
        var result = "";
        result += $"Ідентифікатор: {Id.ToString()}\n";
        result += $"Ім'я: {Name}\n";
        result += $"Електронна адреса: {Email}\n";
        result += $"Данні користувача: {ProfileDetails}\n";
        return result;
    }
}