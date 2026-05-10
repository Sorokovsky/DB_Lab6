// See https://aka.ms/new-console-template for more information

using DB_Lab6.Console;
using DB_Lab6.Console.Commands;

var context = new CommandContext("Головне меню");
context.AddCommand(new ExitCommand()); 
context.Start();