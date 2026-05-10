namespace DB_Lab6.Console.Exceptions;

public class ValidationException : IOException
{
    public ValidationException(string? message) : base(message)
    {
    }
}