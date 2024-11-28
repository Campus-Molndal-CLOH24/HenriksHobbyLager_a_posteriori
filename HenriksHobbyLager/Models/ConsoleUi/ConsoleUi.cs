using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;

namespace HenriksHobbyLager.Models
{
    public class ConsoleUi
    {
        private readonly IProductFacade _productFacade; // Privat fält. Instans av IProductFacade
        private readonly DatabaseType _dbType; // Privat fält. Typ av databas som används

        public
            ConsoleUi(ProductFacade productFacade,
                DatabaseType dbType) // Konstruktor som tar emot en instans av ProductFacade och en instans av DatabaseType.
        {
            _productFacade = productFacade; // Tilldela instanserna till privat fält
            _dbType = dbType; // Tilldela instanserna till privat fält
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
            Console.WriteLine(
                $"Aktiv databas: {_dbType} "); // Skriver ut aktuell databas, Ändra i appsettings.json för att byta databas
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
                case "1": ShowAllProducts(); break; // Visa alla produkter
                case "2": AddProduct(); break; // Lägg till en ny produkt
                case "3": UpdateProduct(); break; // Uppdatera en befintlig produkt
                case "4": DeleteProduct(); break; // Ta bort en produkt
                case "5": SearchProducts(); break; // Sök efter produkter
                case "6": return false; // Avsluta programmet
                default: Console.WriteLine("Ogiltigt val."); break; // Hantera ogiltiga inmatningar
            }
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            return true;
        }
        
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

        private void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Lägg till ny produkt ===");

            var name = UserInputHandler.GetNameInput("Namn");
            var price = UserInputHandler.GetPriceInput("Pris");
            var stock = UserInputHandler.GetStockInput("Lager");
            var category = UserInputHandler.GetCategoryInput("Kategori");

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
            UpdateInput.UpdateName(name, product);

            Console.Write("Nytt pris (enter för att behålla): ");
            var priceInput = Console.ReadLine();
            UpdateInput.UpdatePrice(priceInput, product);

            Console.Write("Ny lagermängd (enter för att behålla): ");
            var stockInput = Console.ReadLine();
            UpdateInput.UpdateStock(stockInput, product);

            Console.Write("Ny kategori (enter för att behålla): ");
            var category = Console.ReadLine();
            UpdateInput.UpdateCategory(category, product);

            _productFacade.UpdateProduct(product);
            Console.WriteLine("Produkten har uppdaterats!");
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