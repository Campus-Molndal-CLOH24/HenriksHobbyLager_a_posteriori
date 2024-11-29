namespace HenriksHobbyLager.Models.ProductServices;

public static class ProductDisplayService
{
    public static void DisplayProduct(Product product)
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