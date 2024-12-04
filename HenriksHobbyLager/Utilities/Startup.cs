using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Utilities
{
    public static class Startup
    {
        public static void Initialize()
        {
            // Läs konfiguration
            var (dbType, connectionString) = ConfigReader.ReadConfig("config.txt");
            IRepository<Product> database;

            if (dbType == "SQL")
            {
                // Använd SQLite-databasen
                var sqliteDatabase = new SqliteDb();
                sqliteDatabase.Connect(connectionString);
                database = sqliteDatabase;
                Console.WriteLine("Anslutning till SQLite lyckades.");
            }
            else if (dbType == "NoSQL")
            {
                // Använd MongoDB
                var mongoDatabase = new MongoDb();
                mongoDatabase.Connect(connectionString);
                database = mongoDatabase;
                Console.WriteLine("Anslutning till MongoDB lyckades.");
            }
            else
            {
                throw new Exception($"Okänd databas typ: {dbType}");
            }

            // Skapa en instans av ProductDatabase (tidigare Database)
            var repository = new Repository(database);

            // Använd ProductFacade för affärslogik
            IProductFacade productFacade = new ProductFacade(repository);

            // Starta menyhanteraren
            var consoleMenuHandler = new ConsoleMenuHandler(productFacade);
            consoleMenuHandler.ShowMainMenu();
        }
    }
}