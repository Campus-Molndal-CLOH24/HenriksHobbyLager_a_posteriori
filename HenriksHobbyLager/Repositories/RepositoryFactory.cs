using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.Extensions.Configuration;

namespace HenriksHobbyLager.Factories
{
    public static class RepositoryFactory
    {
        public static IRepository<Product> CreateRepository(DbTypeEnum dbTypeEnum, IConfiguration configuration)
        {
            // Dictionary för att mappa databastyper till repository-konfigurationer
            var repositoryFactories = new Dictionary<DbTypeEnum, Func<IRepository<Product>>>
            {
                { DbTypeEnum.SQLite, () => SqliteDbSetup.ConfigureSqlite(configuration) },
                { DbTypeEnum.MongoDB, () => MongoDbSetup.ConfigureMongoDb(configuration) }
            };

            // Kontrollera att databastypen stöds
            if (!repositoryFactories.ContainsKey(dbTypeEnum))
            {
                throw new NotSupportedException($"Databastypen {dbTypeEnum} stöds inte.");
            }

            // Returnera rätt repository baserat på databastypen
            return repositoryFactories[dbTypeEnum]();
        }
    }
}