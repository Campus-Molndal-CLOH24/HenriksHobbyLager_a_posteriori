using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HenriksHobbyLager.Models
{
    public class ConsoleUi
    {
        // Fasad för att hantera produktrelaterad logik
        private readonly ProductFacade _productFacade;
        private readonly DatabaseType _dbType;

        // Konstruktor som tar en ProductFacade och en DatabaseType. Möjliggör att kunna skriva ut databasen som används
        public ConsoleUi(ProductFacade productFacade, DatabaseType dbType)
        {
            _productFacade = productFacade;
            _dbType = dbType;
        }

        // Huvudmetoden som kör programmet
        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine($"Aktiv databas: {_dbType} "); // Skriver ut aktuell databas, Ändra i appsettings.json för att byta databas
                Console.WriteLine("=== Henriks Hobby Lager ===");
                Console.WriteLine("1. Visa alla produkter");
                Console.WriteLine("2. Lägg till produkt");
                Console.WriteLine("3. Uppdatera produkt");
                Console.WriteLine("4. Ta bort produkt");
                Console.WriteLine("5. Sök produkter");
                Console.WriteLine("6. Avsluta");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ShowAllProducts(); break; // Visa alla produkter
                    case "2": AddProduct(); break; // Lägg till en ny produkt
                    case "3": UpdateProduct(); break; // Uppdatera en befintlig produkt
                    case "4": DeleteProduct(); break; // Ta bort en produkt
                    case "5": SearchProducts(); break; // Sök efter produkter
                    case "6": running = false; break; // Avsluta programmet
                    default: Console.WriteLine("Ogiltigt val."); break; // Hantera ogiltiga inmatningar
                }

                if (running)
                {
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                }
            }
        }

        // Visar alla produkter som finns i databasen
        private void ShowAllProducts()
        {
            Console.Clear();
            var products = _productFacade.GetAllProducts();

            if (!products.Any())
            {
                Console.WriteLine("Inga produkter finns i lagret.");
                return;
            }

            foreach (var product in products)
            {
                DisplayProduct(product);
            }
        }

        // Lägger till en ny produkt i systemet
        private void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Lägg till ny produkt ===");
    
            var name = GetNameInput("Namn");
            var price = GetPriceInput("Pris");
            var stock = GetStockInput("Lager");
            var category = GetCategoryInput("Kategori");

            var product = new Product
            {
                Name = name,
                Price = price,
                Stock = stock,
                Category = category
            };
    
            _productFacade.CreateProduct(product);
            Console.WriteLine("Produkt tillagd!");
        }

        private string GetNameInput(string name)
        {
            Console.WriteLine($"{name}");
            return Console.ReadLine();
        }

        private decimal GetPriceInput(string price)
        {
            decimal value;
            Console.WriteLine($"{price}");
            while (!decimal.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Ogiltigt pris, försök igen.");
                Console.WriteLine($"{price}");
            }
            return value;
        }
        
        private int GetStockInput(string stock)
        {
            int value;
            Console.WriteLine($"{stock}");
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Ogiltigt lagerantal, försök igen.");
                Console.WriteLine($"{stock}");
            }
            return value;
        }

        private string GetCategoryInput(string category)
        {
            Console.WriteLine($"{category}");
            return Console.ReadLine();
        }

        // Uppdatera en befintlig produkt
        private void UpdateProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Uppdatera produkt ===");

            Console.Write("Ange produkt-ID att uppdatera: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            var product = _productFacade.GetProduct(id);
            if (product == null)
            {
                Console.WriteLine("Produkten hittades inte!");
                return;
            }

            Console.Write("Nytt namn (enter för att behålla): ");
            var name = Console.ReadLine();
            CheckNameInput(name, product);

            Console.Write("Nytt pris (enter för att behålla): ");
            var priceInput = Console.ReadLine();
            CheckPriceInput(priceInput, product);

            Console.Write("Ny lagermängd (enter för att behålla): ");
            var stockInput = Console.ReadLine();
            CheckStockInput(stockInput, product);

            Console.Write("Ny kategori (enter för att behålla): ");
            var category = Console.ReadLine();
            CheckCategoryInput(category, product);

            _productFacade.UpdateProduct(product);
            Console.WriteLine("Produkten har uppdaterats!");
        }

        private static void CheckCategoryInput(string? category, Product product)
        {
            if (!string.IsNullOrWhiteSpace(category)) product.Category = category;
        }

        private static void CheckStockInput(string? stockInput, Product product)
        {
            if (!string.IsNullOrWhiteSpace(stockInput) && int.TryParse(stockInput, out var stock))
                product.Stock = stock;
        }

        private static void CheckNameInput(string? name, Product product)
        {
            if (!string.IsNullOrWhiteSpace(name)) product.Name = name;
        }

        private void CheckPriceInput(string? priceInput, Product product)
        {
            if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out var price))
                product.Price = price;

        }

        // Ta bort en produkt
        private void DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Ta bort produkt ===");

            Console.Write("Ange produkt-ID att ta bort: ");
            if (CheckIdValue(out var id)) return;

            try
            {
                _productFacade.DeleteProduct(id);
                Console.WriteLine("Produkten har tagits bort!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static bool CheckIdValue(out int id)
        {
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Ogiltigt ID!");
                return true;
            }

            return false;
        }

        // Sök efter produkter
        private void SearchProducts()
        {
            Console.Clear();
            Console.Write("Sök: ");
            var searchTerm = Console.ReadLine()?.ToLower();

            var results = _productFacade.SearchProducts(searchTerm);
            if (CheckSearchValue(results)) return;

            foreach (var product in results)
            {
                DisplayProduct(product);
            }
        }

        private static bool CheckSearchValue(IEnumerable<Product> results)
        {
            if (!results.Any())
            {
                Console.WriteLine("Inga produkter matchade din sökning.");
                return true;
            }

            return false;
        }

        // Visar en produkts information
        private void DisplayProduct(Product product)
        {
            Console.WriteLine($"\nID: {product.Id}");
            Console.WriteLine($"Namn: {product.Name}");
            Console.WriteLine($"Pris: {product.Price:C}");
            Console.WriteLine($"Lager: {product.Stock}");
            Console.WriteLine($"Kategori: {product.Category}");
            Console.WriteLine($"Skapad: {product.Created}");
            if (product.LastUpdated.HasValue)
                Console.WriteLine($"Senast uppdaterad: {product.LastUpdated}");
            Console.WriteLine(new string('-', 40));
        }
    }
}