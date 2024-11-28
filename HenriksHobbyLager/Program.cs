using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repositories;

namespace RefactoringExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get database settings from configuration
            IRepository<Product> repository;
            DatabaseType dbType;

            if (Enum.TryParse(configuration["DatabaseSettings:DatabaseType"], out dbType))
            {
                switch (dbType)
                {
                    case DatabaseType.SQLite:
                        var sqliteConnectionString = configuration["DatabaseSettings:SQLiteConnectionString"];
                        DataBaseInit.DataBaseInitialize(sqliteConnectionString);
                        repository = new SqliteProductRepository(sqliteConnectionString);
                        break;

                    case DatabaseType.MongoDB:
                        var mongoConnectionString = configuration["DatabaseSettings:MongoDBConnectionString"];
                        var mongoDatabaseName = configuration["DatabaseSettings:MongoDBDatabaseName"];
                        repository = new MongoDBProductRepository(mongoConnectionString, mongoDatabaseName);
                        break;

                    default:
                        throw new Exception("Ingen Databas vald.");
                }
            }
            else
            {
                throw new Exception("Ogiltig eller saknad databaskonfiguration.");
            }

            // Create ProductFacade with the repository
            var facade = new ProductFacade(repository);

            // Create ConsoleUi with the facade and database type
            var ui = new ConsoleUi(facade, dbType);

            // Start the application
            ui.Run();
        }
    }
}