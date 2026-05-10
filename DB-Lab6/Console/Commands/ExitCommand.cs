namespace DB_Lab6.Console.Commands;

public class ExitCommand : Command
{
    protected override string Name => "Вихід";

    public override void Execute(CommandContext context)
    {
        context.Stop();
    }
}