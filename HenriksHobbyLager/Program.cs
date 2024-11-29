using HenriksHobbyLager.Database;
using HenriksHobbyLager.Factories;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Models.ProductServices;

namespace HenriksHobbyLager
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ladda konfiguration
            var configuration = DbConfig.BuildConfiguration();

            // Kontrollera databastypen
            if (!Enum.TryParse(configuration["DatabaseSettings:DatabaseType"], out DbTypeEnum dbTypeEnum))
            {
                Console.WriteLine("Ogiltig databastyp angiven i appsettings.json. Kontrollera och försök igen.");
                return;
            }

            // Skapa repository baserat på databastyp
            var repository = RepositoryFactory.CreateRepository(dbTypeEnum, configuration);

            // Initiera tjänster och UI
            var productFacade = new ProductFacade(repository);
            var productsServices = new ProductsServices(productFacade);
            var ui = new ConsoleUi(productsServices, dbTypeEnum);

            // Starta programmet
            ui.Run();
        }
    }
}