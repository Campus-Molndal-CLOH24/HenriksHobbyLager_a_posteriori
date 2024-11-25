using RefactoringExercise.Interfaces;

namespace RefactoringExercise.Models
{
    public class ConsoleUi
    {
        // Fasad för att hantera produktrelaterad logik
        private readonly IProductFacade _productFacade;

        // konstruktor för beroendeinjektion av IProductFacade
        public ConsoleUi(IProductFacade productFacade) => _productFacade = productFacade;

        // huvudmetoden som kör programmet
        public void Run()
        {
            bool running = true; 
            while (running)
            {
                Console.Clear();
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
                    // Ge användaren tid att läsa innan skärmen rensas
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                }
            }
        }

        // Visar alla produkter som finns i "databasen"
        private void ShowAllProducts()
        {
            Console.Clear(); 
            var products = _productFacade.GetAllProducts(); // Hämtar alla produkter från facaden

            // Kollar om det finns några produkter alls
            // !_products.Any() låter mer proffsigt än _products.Count == 0
            if (!products.Any())
            {
                Console.WriteLine("Inga produkter finns i lagret.");
                return;
            }

            // Iterera över alla produkter och visa dem
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

            Console.Write("Namn: ");
            var name = Console.ReadLine();
            
            Console.Write("Pris: ");
            if (!decimal.TryParse(Console.ReadLine(), out var price))
            {
                Console.WriteLine("Ogiltigt pris!");
                return;
            }
            
            Console.Write("Antal i lager: ");
            if (!int.TryParse(Console.ReadLine(), out var stock))
            {
                // hantera ogiltiga lagermängder
                Console.WriteLine("Ogiltig lagermängd! Hela tal endast (kan inte sälja halva helikoptrar)");
                return;
            }
            Console.Write("Kategori: ");
            var category = Console.ReadLine();

            // skapa en ny produkt och skicka den till facaden för att sparas
            var product = new Product
            {
                Name = name,
                Price = price,
                Stock = stock,
                Category = category
            };

            _productFacade.CreateProduct(product); // Lägger till produkten i lagret. Ändrat till CreateProduct
            Console.WriteLine("Produkt tillagd!");
        }

        // Uppdatera en befintlig produkt
        private void UpdateProduct()
        {
            Console.Clear(); 
            Console.WriteLine("=== Uppdatera produkt ===");

            Console.Write("Ange produkt-ID att uppdatera (finns i listan ovan): ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            // Hämta produkten med det angivna ID:t
            var product = _productFacade.GetProduct(id);
            if (product == null)
            {
                // Om produkten inte hittas, visa ett felmeddelande
                Console.WriteLine("Produkten hittades inte! Puh, inget blev raderat av misstag!");
                return;
            }

            // uppdatera produktens egenskaper baserat på användarens inmatning
            Console.Write("Nytt namn (enter för att behålla): ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) product.Name = name;

            Console.Write("Nytt pris (enter för att behålla): ");
            var priceInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal price)) product.Price = price;

            Console.Write("Ny lagermängd (enter för att behålla): ");
            var stockInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(stockInput) && int.TryParse(stockInput, out int stock)) product.Stock = stock;

            Console.Write("Ny kategori (enter för att behålla): ");
            var category = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(category)) product.Category = category;

            // uppdatera produkten via facaden
            _productFacade.UpdateProduct(product);
            Console.WriteLine("Produkten har uppdaterats!");
        }

        // ta bort en produkt
        private void DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Ta bort produkt ===");

            Console.Write("Ange produkt-ID att ta bort (dubbel-check att det är rätt, går inte att ångra!): ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Ogiltigt ID! Bara siffror är tillåtna här.");
                return;
            }

            try
            {
                // försök ta bort produkten via facaden
                _productFacade.DeleteProduct(id);
                Console.WriteLine("Produkten har tagits bort!");
            }
            catch (ArgumentException ex)
            {
                // Hantera undantag om produkten inte hittas
                Console.WriteLine(ex.Message);
            }
        }

        // Sök efter produkter baserat på namn eller kategori
        private void SearchProducts()
        {
            Console.Clear(); // Rensa skärmen innan sökningen startar
            Console.Write("Sök (namn eller kategori - versaler spelar ingen roll!): ");
            var searchTerm = Console.ReadLine().ToLower();

            // Hämta sökresultat från facaden
            var results = _productFacade.SearchProducts(searchTerm);

            if (!results.Any())
            {
                // Visa ett meddelande om inga produkter matchar
                Console.WriteLine("Inga produkter matchade din sökning.");
                return;
            }

            // Visa varje produkt som matchade sökningen
            foreach (var product in results)
            {
                DisplayProduct(product);
            }
        }

        // metod för att visa en produkts information
        private void DisplayProduct(Product product)
        {
            // Snygga streck som separerar produkterna
            Console.WriteLine($"\nID: {product.Id}");
            Console.WriteLine($"Namn: {product.Name}");
            Console.WriteLine($"Pris: {product.Price:C}");  // :C gör att det blir kronor automatiskt!
            Console.WriteLine($"Lager: {product.Stock}");
            Console.WriteLine($"Kategori: {product.Category}");
            Console.WriteLine($"Skapad: {product.Created}");
            if (product.LastUpdated.HasValue)  // Kollar om produkten har uppdaterats någon gång
                Console.WriteLine($"Senast uppdaterad: {product.LastUpdated}");
            Console.WriteLine(new string('-', 40));  // Snyggt streck mellan produkterna
        }        
    }
}