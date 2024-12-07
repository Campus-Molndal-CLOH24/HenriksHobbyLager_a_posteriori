using HenriksHobbyLager.Database;
using HenriksHobbyLager.Database.Repositories;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Services;
using HenriksHobbyLager.UI;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Utilities
{
    public static class DatabaseConfiguration
    {
        public static void Initialize()
        {
            // Läser konfiguration för att avgöra databasens typ och anslutningssträng
            var (dbType, connectionString) = ConfigReader.ReadConfig("config.txt");
            IRepository<Product> database;

            if (dbType == "SQL")
            {
                // Konfigurerar SQLite-databasen med Entity Framework Core
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlite(connectionString)
                    .Options;

                var sqliteDatabase = new SqliteProductRepository(new AppDbContext(options));
                database = sqliteDatabase;
                Console.WriteLine("Anslutning till SQLite lyckades.");
            }
            else if (dbType == "NoSQL")
            {
                // Konfigurerar MongoDB-databasen
                var mongoDatabase = new MongoDbProductRepository();
                mongoDatabase.Connect(connectionString);
                database = mongoDatabase;
                Console.WriteLine("Anslutning till MongoDB lyckades.");
            }
            else
            {
                // Kastar ett undantag om databasens typ är okänd
                throw new Exception($"Okänd databas typ: {dbType}");
            }

            // Skapar ProductService som en mellanhand för databasen
            IProductService productService = new ProductService(database);

            // Skapar och startar ConsoleMenuHandler med vald databas
            var consoleMenuHandler = new ConsoleMenuHandler(productService, dbType);
            consoleMenuHandler.ShowMainMenu();
        }
    }
}