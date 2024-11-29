using HenriksHobbyLager.Interfaces;

namespace HenriksHobbyLager.Models.ProductServices;

public class ProductsServices
{
    private readonly IProductFacade _productFacade;

    public ProductsServices(IProductFacade productFacade)
    {
        _productFacade = productFacade;
    }

    public void ShowAllProducts()
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
            ProductDisplayService.DisplayProduct(product);
        }
    }

    public void AddProduct()
    {
        Console.Clear();
        Console.WriteLine("=== Lägg till ny produkt ===");

        var name = ConsoleInputHandler.GetInput("Namn");
        var price = ConsoleInputHandler.GetDecimalInput("Pris");
        var stock = ConsoleInputHandler.GetIntInput("Lager");
        var category = ConsoleInputHandler.GetInput("Kategori");

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

    public void UpdateProduct()
    {
        Console.Clear();
        Console.WriteLine("=== Uppdatera produkt ===");

        Console.Write("Ange produkt-ID att uppdatera: ");
        if (!int.TryParse(Console.ReadLine(), out var id)) // Om ID inte är int
        {
            Console.WriteLine("Ogiltigt ID!");
            return;
        }

        var product = _productFacade.GetProduct(id); 
        if (product == null) // Om ID inte finns i databasen
        {
            Console.WriteLine("Produkten hittades inte!");
            return;
        }

        var name = ConsoleInputHandler.SetOptionalInput("Nytt namn (enter för att behålla)");
        if (!string.IsNullOrWhiteSpace(name)) product.Name = name;

        var priceInput = ConsoleInputHandler.SetOptionalDecimalInput("Nytt pris (enter för att behålla)");
        if (priceInput.HasValue) product.Price = priceInput.Value;

        var stockInput = ConsoleInputHandler.SetOptionalIntInput("Ny lagermängd (enter för att behålla)");
        if (stockInput.HasValue) product.Stock = stockInput.Value;

        var category = ConsoleInputHandler.SetOptionalInput("Ny kategori (enter för att behålla)");
        if (!string.IsNullOrWhiteSpace(category)) product.Category = category;

        _productFacade.UpdateProduct(product);
        Console.WriteLine("Produkten har uppdaterats!");
    }

    public void DeleteProduct()
    {
        Console.Clear();
        Console.WriteLine("=== Ta bort produkt ===");

        Console.Write("Ange produkt-ID att ta bort: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Ogiltigt ID!");
            return;
        }

        _productFacade.DeleteProduct(id);
        Console.WriteLine("Produkten har tagits bort!");
    }

    public void SearchProducts()
    {
        Console.Clear();
        Console.Write("Sök: ");
        var searchTerm = Console.ReadLine()?.ToLower();

        var results = _productFacade.SearchProducts(searchTerm);
        if (!results.Any())
        {
            Console.WriteLine("Inga produkter matchade din sökning.");
            return;
        }

        foreach (var product in results)
        {
            ProductDisplayService.DisplayProduct(product);
        }
    }
}