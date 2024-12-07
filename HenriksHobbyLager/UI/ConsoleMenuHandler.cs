using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.UI
{
    public class ConsoleMenuHandler
    {
        private readonly IProductService _productService;
        private readonly string _databaseType; // Håller information om vilken databas som används (SQL eller NoSQL)

        public ConsoleMenuHandler(IProductService productService, string databaseType)
        {
            _productService = productService;
            _databaseType = databaseType;
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Databas: {_databaseType}"); // Informerar användaren om vilken databas som är aktiv
                Console.WriteLine("=== Henriks HobbyLager™ 2.0 ===");
                Console.WriteLine("1. Visa alla produkter");
                Console.WriteLine("2. Lägg till produkt");
                Console.WriteLine("3. Uppdatera produkt");
                Console.WriteLine("4. Ta bort produkt");
                Console.WriteLine("5. Sök efter produkt");
                Console.WriteLine("6. Avsluta");

                var choice = Console.ReadLine();
                HandleMenuChoice(choice); // Hanterar användarens val i menyn

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private async Task HandleMenuChoice(string choice)
        {
            // Anropar en specifik metod baserat på användarens menyval
            switch (choice)
            {
                case "1": await ShowAllProducts(); break;
                case "2": await AddProduct(); break;
                case "3": await UpdateProduct(); break;
                case "4": await DeleteProduct(); break;
                case "5": await SearchProducts(); break;
                case "6": Environment.Exit(0); break;
                default: Console.WriteLine("Ogiltigt val, försök igen."); break;
            }
        }

        private async Task ShowAllProducts()
        {
            // Hämtar alla produkter via ProductService och skriver ut dem
            var products = await _productService.GetAllProducts();
            if (!products.Any())
            {
                Console.WriteLine("Inga produkter hittades.");
                return;
            }

            foreach (var product in products)
            {
                PrintProducts(product); // Skriver ut detaljer för varje produkt
            }
        }

        private Task AddProduct()
        {
            // Tar in användarens indata för att skapa en ny produkt
            Console.Write("Namn: ");
            string name = Console.ReadLine();

            decimal price;
            while (true)
            {
                Console.Write("Pris: ");
                if (decimal.TryParse(Console.ReadLine(), out price)) break;

                Console.WriteLine("Felaktig inmatning för pris. Försök igen!");
            }

            int stock;
            while (true)
            {
                Console.Write("Lagersaldo: ");
                if (int.TryParse(Console.ReadLine(), out stock)) break;

                Console.WriteLine("Felaktig inmatning för lagersaldo. Försök igen!");
            }

            Console.Write("Kategori: ");
            string category = Console.ReadLine();

            // Skapar ett nytt produktobjekt med den inmatade datan
            var product = new Product
            {
                Name = name,
                Price = price,
                Stock = stock,
                Category = category,
                Created = DateTime.Now
            };

            _productService.CreateProduct(product); // Lägger till produkten i databasen via ProductService
            Console.WriteLine("Produkten har lagts till.");
            return Task.CompletedTask;
        }

        private async Task UpdateProduct()
        {
            // Hämtar en produkt baserat på ID och tillåter användaren att uppdatera den
            Console.Write("Ange produktens ID: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Felaktigt ID.");
                return;
            }

            var product = await _productService.GetProduct(id); // Hämtar produkten från databasen
            if (product == null)
            {
                Console.WriteLine("Produkten hittades inte.");
                return;
            }

            // Uppdaterar endast de fält användaren anger
            Console.Write("Nytt namn (lämna tomt för att behålla): ");
            string name = Console.ReadLine();

            Console.Write("Nytt pris (lämna tomt för att behålla): ");
            string priceInput = Console.ReadLine();

            Console.Write("Nytt lagersaldo (lämna tomt för att behålla): ");
            string stockInput = Console.ReadLine();

            Console.Write("Ny kategori (lämna tomt för att behålla): ");
            string category = Console.ReadLine();

            product.Name = string.IsNullOrEmpty(name) ? product.Name : name;
            product.Price = string.IsNullOrEmpty(priceInput) ? product.Price : decimal.Parse(priceInput);
            product.Stock = string.IsNullOrEmpty(stockInput) ? product.Stock : int.Parse(stockInput);
            product.Category = string.IsNullOrEmpty(category) ? product.Category : category;
            product.Updated = DateTime.Now;

            _productService.UpdateProduct(product); // Sparar uppdateringarna i databasen
            Console.WriteLine("Produkten har uppdaterats.");
        }

        private async Task DeleteProduct()
        {
            // Tar bort en produkt baserat på ID
            Console.Write("Ange produktens ID: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Felaktigt ID.");
                return;
            }

            var product = await _productService.GetProduct(id); // Hämtar produkten från databasen
            if (product == null)
            {
                Console.WriteLine("Produkten hittades inte.");
                return;
            }

            _productService.DeleteProduct(id); // Tar bort produkten från databasen via ProductService
            Console.WriteLine("Produkten har tagits bort.");
        }

        private async Task SearchProducts()
        {
            // Söker efter produkter baserat på användarens sökterm
            Console.Write("Sökterm: ");
            string searchTerm = Console.ReadLine();

            var results = await _productService.SearchProducts(searchTerm); // Hämtar produkter som matchar söktermen
            if (!results.Any())
            {
                Console.WriteLine("Inga produkter hittades.");
                return;
            }

            foreach (var product in results)
            {
                PrintProducts(product); // Skriver ut detaljer för varje matchande produkt
            }
        }

        private static void PrintProducts(Product product)
        {
            // Skriver ut information om en enskild produkt
            Console.WriteLine(
                $"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}kr, Lagerantal: {product.Stock}st, Kategori: {product.Category}");
        }
    }
}