using MongoDB.Driver;

namespace DB_Lab6;

public static class SharedData
{
    public static MongoClient Client { get; set; }

}