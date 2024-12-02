using HenriksHobbyLager.Repositories;
using HenriksHobbyLager.Services;
using HenriksHobbyLager.Helpers;

namespace HenriksHobbyLager.ProgramManagement
{
    internal class HenriksHobbyLagerProgramManager
    {
        public void Run()
        {
            // Testa anslutningen till databasen
            using (var context = new AppDbContext())
            {
                var toys = context.Products.ToList(); // Hämta alla rader från tabellen
                Console.WriteLine($"Hittade {toys.Count} leksaker i databasen!");

                foreach (var toy in toys)
                {
                    Console.WriteLine($"Name: {toy.Name}, Price: {toy.Price}, Stock: {toy.Stock}");
                }
            }

            // Starta repositories, services och menyhanteraren
            var repository = new ProductRepository(); // Hanterar data
            var productService = new ProductService(repository); // Hanterar logik
            var menuHandler = new MenuHandler(productService); // Hanterar menyval

            // Kör huvudloopen för menyhantering
            while (true)
            {
                menuHandler.ShowMenu(); // Visa menyn
                var choice = ConsoleHelper.ReadInput("Välj ett alternativ"); // Läs in val
                menuHandler.HandleMenuChoice(choice); // Hantera valet
            }
        }
    }
}
