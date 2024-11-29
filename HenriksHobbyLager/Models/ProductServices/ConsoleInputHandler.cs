namespace HenriksHobbyLager.Models;

public static class ConsoleInputHandler
{
    public static string GetInput(string prompt)
    {
        Console.Write($"{prompt}: ");
        return Console.ReadLine();
    }

    public static decimal GetDecimalInput(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            if (decimal.TryParse(Console.ReadLine(), out var value))
                return value;

            Console.WriteLine("Ogiltigt värde, försök igen.");
        }
    }

    public static int GetIntInput(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            if (int.TryParse(Console.ReadLine(), out var value))
                return value;

            Console.WriteLine("Ogiltigt värde, försök igen.");
        }
    }

    public static string SetOptionalInput(string prompt)
    {
        Console.Write($"{prompt}: ");
        return Console.ReadLine();
    }

    public static decimal? SetOptionalDecimalInput(string prompt)
    {
        Console.Write($"{prompt}: ");
        if (decimal.TryParse(Console.ReadLine(), out var value))
            return value;

        return null;
    }

    public static int? SetOptionalIntInput(string prompt)
    {
        Console.Write($"{prompt}: ");
        if (int.TryParse(Console.ReadLine(), out var value))
            return value;

        return null;
    }
}