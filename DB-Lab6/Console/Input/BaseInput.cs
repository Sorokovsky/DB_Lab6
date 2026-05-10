namespace DB_Lab6.Console.Input;

public abstract class BaseInput<T>
{
    public virtual T Enter(string question)
    {
        System.Console.Write(question);
        return Transform(System.Console.ReadLine() ?? string.Empty);
    }

    protected abstract T Transform(string raw);
}