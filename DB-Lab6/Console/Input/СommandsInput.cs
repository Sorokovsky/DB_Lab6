using DB_Lab6.Console.Commands;
using DB_Lab6.Console.Exceptions;

namespace DB_Lab6.Console.Input;

public class СommandsInput : BaseInput<Command>
{
    private readonly List<Command> _commands;

    public Command Enter()
    {
        return base.Enter(">> ");
    }

    public СommandsInput(List<Command> commands)
    {
        _commands = commands;
    }

    protected override Command Transform(string raw)
    {
        try
        {
            var command = _commands.Find(command => command.Id == int.Parse(raw));
            return command ?? throw new CommandException();
        } catch(FormatException)
        {
            throw new CommandException();
        }
    }
}