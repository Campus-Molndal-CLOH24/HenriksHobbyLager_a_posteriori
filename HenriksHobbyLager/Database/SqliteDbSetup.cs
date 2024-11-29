using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repositories;
using Microsoft.Extensions.Configuration;

namespace HenriksHobbyLager.Database
{
    public static class SqliteDbSetup
    {
        public static IRepository<Product> ConfigureSqlite(IConfiguration configuration)
        {
            var sqliteConnectionString = configuration["DatabaseSettings:SQLiteConnectionString"]; // Hämtar anslutningssträngen för SQLite från konfigurationen (appsettings.json).
            DbInitializer.DataBaseInitialize(sqliteConnectionString);    // Initierar SQLite-databasen (om det behövs, dvs skapar tabeller).
            return new SqliteProductRepository(sqliteConnectionString); //  Returnerar en instans av SqliteProductRepository, som används för att hantera SQLite-databasanrop.
        }
    }
}