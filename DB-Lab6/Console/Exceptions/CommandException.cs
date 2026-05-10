namespace DB_Lab6.Console.Exceptions;

public class CommandException : ValidationException
{
    public CommandException() : base("Команду не розпізнано, спробуйте ше.")
    {
    }
}