using HenriksHobbyLager.Database.Repositories;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Services;
using HenriksHobbyLager.UI;

namespace HenriksHobbyLager.Utilities
{
    public static class DatabaseConfiguration
    {
        public static void Initialize()
        {
            // Läs konfiguration
            var (dbType, connectionString) = ConfigReader.ReadConfig("config.txt");
            IRepository<Product> database;

            if (dbType == "SQL")
            {
                var sqliteDatabase = new SqliteProductRepository();
                sqliteDatabase.Connect(connectionString);
                database = sqliteDatabase;
                Console.WriteLine("Anslutning till SQLite lyckades.");
            }
            else if (dbType == "NoSQL")
            {
                var mongoDatabase = new MongoDbProductRepository();
                mongoDatabase.Connect(connectionString);
                database = mongoDatabase;
                Console.WriteLine("Anslutning till MongoDB lyckades.");
            }
            else
            {
                throw new Exception($"Okänd databas typ: {dbType}");
            }

            IProductService productService = new ProductService(database);
            // Skicka databasens typ till ConsoleMenuHandler
            var consoleMenuHandler = new ConsoleMenuHandler(productService, dbType);
            consoleMenuHandler.ShowMainMenu();
        }
    }
}