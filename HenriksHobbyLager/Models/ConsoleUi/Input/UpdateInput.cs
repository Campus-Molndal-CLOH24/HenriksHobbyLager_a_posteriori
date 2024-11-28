namespace HenriksHobbyLager.Models;

public class UpdateInput
{
    public static void UpdateName(string? name, Product product)
    {
        if (!string.IsNullOrWhiteSpace(name)) product.Name = name;
    }

    public static void UpdatePrice(string? priceInput, Product product)
    {
        if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out var price))
            product.Price = price;
    }

    public static void UpdateStock(string? stockInput, Product product)
    {
        if (!string.IsNullOrWhiteSpace(stockInput) && int.TryParse(stockInput, out var stock))
            product.Stock = stock;
    }
    public static void UpdateCategory(string? category, Product product)
    {
        if (!string.IsNullOrWhiteSpace(category)) product.Category = category;
    }

}