using DB_Lab6;
using DB_Lab6.Console.Commands;
using MongoDB.Driver;

const string connectionUri = "mongodb://localhost:27017";
SharedData.Client = new MongoClient(connectionUri);

var context = new CommandContext("Головне меню");
context.AddCommand(new ExitCommand()); 
context.Start();