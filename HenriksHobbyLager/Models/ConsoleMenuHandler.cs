using System.Data;
using HenriksHobbyLager.Interfaces;

namespace HenriksHobbyLager.Models
{
    public class ConsoleMenuHandler
    {
        private readonly IProductFacade _productFacade;
        
        private readonly DbType dbType;
        

        public ConsoleMenuHandler(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine(dbType.GetType()  );
                Console.WriteLine("=== Henriks HobbyLager™ 2.0 ===");
                Console.WriteLine("1. Visa alla produkter");
                Console.WriteLine("2. Lägg till produkt");
                Console.WriteLine("3. Uppdatera produkt");
                Console.WriteLine("4. Ta bort produkt");
                Console.WriteLine("5. Sök efter produkt");
                Console.WriteLine("6. Avsluta");

                var choice = Console.ReadLine();
                HandleMenuChoice(choice);

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private void HandleMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1":ShowAllProducts();break;
                case "2":AddProduct();break;
                case "3":UpdateProduct();break;
                case "4":DeleteProduct();break;
                case "5":SearchProducts();break;
                case "6":Environment.Exit(0);break;
                default:Console.WriteLine("Ogiltigt val, försök igen.");break;
            }
        }

        private void ShowAllProducts()
        {
            var products = _productFacade.GetAllProducts();
            if (!products.Any())
            {
                Console.WriteLine("Inga produkter hittades.");
                return;
            }

            foreach (var product in products)
            {
                PrintProducts(product);
            }
        }


        private void AddProduct()
        {
            Console.Write("Namn: ");
            string name = Console.ReadLine();

            Console.Write("Pris: ");
            if (!decimal.TryParse(Console.ReadLine(), out var price))
            {
                Console.WriteLine("Felaktig inmatning för pris.");
                return;
            }

            Console.Write("Lagersaldo: ");
            if (!int.TryParse(Console.ReadLine(), out var stock))
            {
                Console.WriteLine("Felaktig inmatning för lagersaldo.");
                return;
            }

            Console.Write("Kategori: ");
            string category = Console.ReadLine();

            var product = new Product
            {
                Name = name,
                Price = price,
                Stock = stock,
                Category = category,
                Created = DateTime.Now
            };

            _productFacade.CreateProduct(product);
            Console.WriteLine("Produkten har lagts till.");
        }

        private void UpdateProduct()
        {
            Console.Write("Ange produktens ID: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Felaktigt ID.");
                return;
            }

            Console.Write("Nytt namn (lämna tomt för att behålla): ");
            string name = Console.ReadLine();

            Console.Write("Nytt pris (lämna tomt för att behålla): ");
            string priceInput = Console.ReadLine();

            Console.Write("Nytt lagersaldo (lämna tomt för att behålla): ");
            string stockInput = Console.ReadLine();

            Console.Write("Ny kategori (lämna tomt för att behålla): ");
            string category = Console.ReadLine();

            var product = _productFacade.GetProduct(id);
            if (product == null)
            {
                Console.WriteLine("Produkten hittades inte.");
                return;
            }

            product.Name = string.IsNullOrEmpty(name) ? product.Name : name;
            product.Price = string.IsNullOrEmpty(priceInput) ? product.Price : decimal.Parse(priceInput);
            product.Stock = string.IsNullOrEmpty(stockInput) ? product.Stock : int.Parse(stockInput);
            product.Category = string.IsNullOrEmpty(category) ? product.Category : category;
            product.Updated = DateTime.Now;

            _productFacade.UpdateProduct(product);
            Console.WriteLine("Produkten har uppdaterats.");
        }

        private void DeleteProduct()
        {
            Console.Write("Ange produktens ID: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Felaktigt ID.");
                return;
            }

            _productFacade.DeleteProduct(id);
            Console.WriteLine("Produkten har tagits bort.");
        }

        private void SearchProducts()
        {
            Console.Write("Sökterm: ");
            string searchTerm = Console.ReadLine();

            var results = _productFacade.SearchProducts(searchTerm);
            if (!results.Any())
            {
                Console.WriteLine("Inga produkter hittades.");
                return;
            }

            foreach (var product in results)
            {
                PrintProducts(product);
            }
        }
        private static void PrintProducts(Product product)
        {
            Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}kr, Lagerantal: {product.Stock}st, Kategori: {product.Category}");
        }
    }
}
