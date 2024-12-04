using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Utilities
{
    public static class DbInitializer
    {
        public static void Initialize()
        {
            // Läs konfiguration
            var (dbType, connectionString) = ConfigReader.ReadConfig("config.txt");
            IRepository<Product> database;

            if (dbType == "SQL")
            {
                var sqliteDatabase = new SqliteDb();
                sqliteDatabase.Connect(connectionString);
                database = sqliteDatabase;
                Console.WriteLine("Anslutning till SQLite lyckades.");
            }
            else if (dbType == "NoSQL")
            {
                var mongoDatabase = new MongoDb();
                mongoDatabase.Connect(connectionString);
                database = mongoDatabase;
                Console.WriteLine("Anslutning till MongoDB lyckades.");
            }
            else
            {
                throw new Exception($"Okänd databas typ: {dbType}");
            }

            IProductFacade productFacade = new ProductFacade(database);
            // Skicka databasens typ till ConsoleMenuHandler
            var consoleMenuHandler = new ConsoleMenuHandler(productFacade, dbType);
            consoleMenuHandler.ShowMainMenu();
        }
    }
}