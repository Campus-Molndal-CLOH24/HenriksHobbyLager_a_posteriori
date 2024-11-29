using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models.ProductServices;

namespace HenriksHobbyLager.Models
{
    public class ConsoleMenuHandler
    {
        private readonly ProductsServices _productsServices; // Privat fält för ProductsServices
        private readonly DbTypeEnum _dbTypeEnum; // Privat fält för aktuell databas

        // Konstruktor som tar emot ProductsServices och DatabaseType
        public ConsoleMenuHandler(ProductsServices productsServices, DbTypeEnum dbTypeEnum)
        {
            _productsServices = productsServices; // Tilldelar services-instansen
            _dbTypeEnum = dbTypeEnum; // Tilldelar databasens typ
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                ShowMenu();
                var choice = Console.ReadLine();
                running = HandleMenuChoice(choice);
            }
        }

        private void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine($"Aktiv databas: {_dbTypeEnum}"); // Visar aktiv databas
            Console.WriteLine("=== Henriks Hobby Lager ===");
            Console.WriteLine("1. Visa alla produkter");
            Console.WriteLine("2. Lägg till produkt");
            Console.WriteLine("3. Uppdatera produkt");
            Console.WriteLine("4. Ta bort produkt");
            Console.WriteLine("5. Sök produkter");
            Console.WriteLine("6. Avsluta");
        }

        private bool HandleMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1": _productsServices.ShowAllProducts(); break; // Visa alla produkter
                case "2": _productsServices.AddProduct(); break; // Lägg till en ny produkt
                case "3": _productsServices.UpdateProduct(); break; // Uppdatera en befintlig produkt
                case "4": _productsServices.DeleteProduct(); break; // Ta bort en produkt
                case "5": _productsServices.SearchProducts(); break; // Sök efter produkter
                case "6": return false; // Avsluta programmet
                default: Console.WriteLine("Ogiltigt val."); break; // Hantera ogiltiga inmatningar
            }
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            return true;
        }
    }
}