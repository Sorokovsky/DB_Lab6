namespace DB_Lab6.Console.Commands;

public abstract class Command
{
    private int _id;

    public int Id => _id;
    
    protected abstract string Name { get; }
    
    public string Title => $"{Id}-{Name}.";
    
    public abstract void Execute(CommandContext context);

    public void SetId(int id)
    {
        _id = id;
    }
}