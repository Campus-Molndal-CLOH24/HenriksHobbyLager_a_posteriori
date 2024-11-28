using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repositories;
using Microsoft.Extensions.Configuration;

namespace HenriksHobbyLager.Database
{
    public static class SqliteConfigurator
    {
        public static IRepository<Product> ConfigureSqlite(IConfiguration configuration)
        {
            // Hämta SQLite-anslutningssträngen
            var sqliteConnectionString = configuration["DatabaseSettings:SQLiteConnectionString"];
            if (string.IsNullOrWhiteSpace(sqliteConnectionString))
            {
                throw new ArgumentException("SQLite-anslutningssträngen är inte konfigurerad eller saknas i appsettings.json.");
            }

            // Initiera databasen
            DataBaseInit.DataBaseInitialize(sqliteConnectionString);

            // Returnera repository
            return new SqliteProductRepository(sqliteConnectionString);
        }
    }
}