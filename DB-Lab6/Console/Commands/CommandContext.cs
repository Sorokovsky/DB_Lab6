using System.Text;
using DB_Lab6.Console.Exceptions;
using DB_Lab6.Console.Input;

namespace DB_Lab6.Console.Commands;

public class CommandContext : Command
{
    private readonly LinkedList<Command> _commands = [];
    private СommandsInput _input;
    private bool _isRunning;
    private Encoding inputEncoding;
    private Encoding outputEncoding;
    private readonly IServiceProvider _serviceProvider;
    
    public IServiceProvider ServiceProvider => _serviceProvider;
    
    protected override string Name { get; }

    public CommandContext(string name, IServiceProvider serviceProvider)
    {
        Name = name;
        _serviceProvider = serviceProvider;
    }

    public void AddCommand(Command command)
    {
        _commands.AddLast(command);
    }
    
    public override void Execute(CommandContext context)
    {
        Start();
    }

    public void Start()
    {
        _input = new СommandsInput(_commands.ToList());
        _isRunning = true;
        inputEncoding = System.Console.InputEncoding;
        outputEncoding = System.Console.OutputEncoding;
        System.Console.InputEncoding = Encoding.UTF8;
        System.Console.OutputEncoding = Encoding.UTF8;
        Loop();
    }

    private void Loop()
    {
        while (_isRunning)
        {
            try
            {
                PrepareCommands();
                ShowCommands();
                _input.Enter().Execute(this);
            }
            catch (ValidationException exception)
            {
                System.Console.WriteLine(exception.Message);
            }
            catch (Exception exception)
            {
                System.Console.WriteLine($"Невідома помилка({exception.Message}), спробуйте ще");
            }
        }
    }

    private void PrepareCommands()
    {
        var i = 0;
        foreach (var command in _commands)
        {
            command.SetId(i++);
        }
    }


    private void ShowCommands()
    {
        System.Console.WriteLine($"Меню: '{Name}'");
        foreach (var command in _commands)
        {
            System.Console.WriteLine(command.Title);
        }
    }

    public void Stop()
    {
        _isRunning = false;
        System.Console.InputEncoding = inputEncoding;
        System.Console.OutputEncoding = outputEncoding;
    }
}