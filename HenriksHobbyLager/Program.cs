/*
testdata:
var anka = new Product
{
    Name = "Gummianka",
    Price = 10,
    Stock = 10,
    Category = "programmering"
};
*/

/*
    HENRIKS HOBBYLAGER™ 1.0
    Skapat av: Henrik Hobbykodare
    Datum: En sen kväll i oktober efter fyra Red Bull
    Version: 1.0 (eller kanske 1.1, jag har ändrat lite sen första versionen)

    TODO-lista:
    * Kolla vad interfaces egentligen gör
    * Fixa så att datan inte försvinner när datorn stängs av
    * Lägga till stöd för bilder på produkterna (kanske)
    * Göra backups (förlorade nästan allt förra veckan när skärmsläckaren startade)
    * Kolla upp det där med "molnet" som alla pratar om
    * Snygga till koden (när jag har tid)
    * Lägg till ljudeffekter när man lägger till produkter???
    * Fixa så att programmet startar automatiskt när datorn startar om
    * Be någon förklara vad "dependency injection" betyder
    * Köpa en UPS (strömavbrott är INTE kul!)
    * Lära mig vad XML är (folk säger att det är viktigt)
    * Göra en logga till programmet i Paint
    
    VIKTIGT: Stäng inte av datorn! All data ligger i minnet!
    
    PS. Om någon hittar det här i framtiden: Jag vet att koden kunde varit snyggare, 
    men den fungerar! Och det är huvudsaken... right?
*/

using HenriksHobbyLager.Repositories;
using HenriksHobbyLager.Services;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var repository = new ProductRepository(); // Repository för att hantera data
        var service = new ProductService(repository); // Service för att hantera logik

        while (true)
        {
            ShowMenu(); // Visa menyval för användaren
            var choice = Console.ReadLine(); // Läs användarens val

            HandleMenuChoice(choice, service); // Hantera valet med hjälp av service
        }
    }

    // Visar huvudmenyn
    private static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Henriks HobbyLager™ ===");
        Console.WriteLine("1. Visa alla produkter");
        Console.WriteLine("2. Lägg till produkt");
        Console.WriteLine("3. Uppdatera produkt");
        Console.WriteLine("4. Ta bort produkt");
        Console.WriteLine("5. Avsluta");
        Console.Write("Välj ett alternativ: ");
    }

    // Hanterar menyval
    private static void HandleMenuChoice(string choice, ProductService service)
    {
        switch (choice)
        {
            case "1":
                service.ShowAllProducts();
                break;
            case "2":
                service.AddProduct();
                break;
            case "3":
                service.UpdateProduct();
                break;
            case "4":
                service.DeleteProduct();
                break;
            case "5":
                Console.WriteLine("Avslutar programmet...");
                Environment.Exit(0); // Avslutar programmet
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;
        }

        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }
}

