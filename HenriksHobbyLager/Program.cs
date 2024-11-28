using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.Extensions.Configuration;

namespace HenriksHobbyLager
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = BuildConfig.BuildConfiguration();

            DatabaseType dbType;
            IRepository<Product> repository;

            if (!Enum.TryParse(configuration["DatabaseSettings:DatabaseType"], out dbType))
            {
                Console.WriteLine("Ogiltig databastyp angiven i appsettings.json. Kontrollera och försök igen.");
                return;
            }
            
            var repositoryFactories = new Dictionary<DatabaseType, Func<IRepository<Product>>> // Skapa en dictionary med databastyp och en funktion som returnerar ett repository för respektive databastyp
            {
                { DatabaseType.SQLite, () => SqliteDatabaseSetup.ConfigureSqlite(configuration) },
                { DatabaseType.MongoDB, () => MongoDbDatabaseSetup.ConfigureMongoDb(configuration) }
            };

            // Tilldela repository baserat på vald databastyp
            repository = repositoryFactories[dbType]();

            // Skapa facade och användargränssnitt och starta programmet
            var facade = new ProductFacade(repository);
            var ui = new ConsoleUi(facade, dbType);
            ui.Run();
        }
    }
}
