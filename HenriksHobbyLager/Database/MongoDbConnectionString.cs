using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repositories;
using Microsoft.Extensions.Configuration;

namespace HenriksHobbyLager.Database;

public static class MongoDbConnectionString
{
    public static IRepository<Product> ConfigureMongoDb(IConfiguration configuration)
    {
        var mongoConnectionString = configuration["DatabaseSettings:MongoDBConnectionString"]; // Hämtar anslutningssträngen och 
        var mongoDatabaseName = configuration["DatabaseSettings:MongoDBDatabaseName"]; // databasnamnet för MongoDB från konfigurationen (appsettings.json).
        
        // Behövs ingen initiering av databasen då den är schemallös om jag har förstått rätt
        return new MongoDBProductRepository(mongoConnectionString, mongoDatabaseName);                         // Returnerar en instans av MongoDBProductRepository, som används för att hantera MongoDB-databasanrop.
    }
}