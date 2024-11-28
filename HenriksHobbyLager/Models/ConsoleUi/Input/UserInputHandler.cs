namespace HenriksHobbyLager.Models;

public class UserInputHandler
{
    public static string GetNameInput(string name)
    {
        Console.WriteLine($"{name}");
        return Console.ReadLine();
    }

    public static decimal GetPriceInput(string price)
    {
        decimal value;
        Console.WriteLine($"{price}");
        while (!decimal.TryParse(Console.ReadLine(), out value)) // out säkerställer att värdet i variabeln value tilldelas i metoden
        {
            Console.WriteLine("Ogiltigt pris, försök igen.");
            Console.WriteLine($"{price}");
        }
        return value;
    }

    public static int GetStockInput(string stock)
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

    public static string GetCategoryInput(string category)
    {
        Console.WriteLine($"{category}");
        return Console.ReadLine();
    }

}