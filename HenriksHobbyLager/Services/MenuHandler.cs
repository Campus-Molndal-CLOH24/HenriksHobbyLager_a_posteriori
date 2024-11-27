using HenriksHobbyLager.Services;
using HenriksHobbyLager.Helpers;
using System;

namespace HenriksHobbyLager
{
    public class MenuHandler // Hanterar ShowMenu och HandleMenuChoice istället för att ha dem i Main. 
    {
        private readonly ProductService _service;

        public MenuHandler(ProductService service)
        {
            _service = service;
        }

        public void ShowMenu()
        {
            ConsoleHelper.PrintMessage("=== Henriks HobbyLager™ ===");
            ConsoleHelper.PrintMessage("1. Visa alla produkter");
            ConsoleHelper.PrintMessage("2. Lägg till produkt");
            ConsoleHelper.PrintMessage("3. Uppdatera produkt");
            ConsoleHelper.PrintMessage("4. Ta bort produkt");
            ConsoleHelper.PrintMessage("5. Avsluta");
        }

        public void HandleMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    _service.ShowAllProducts();
                    break;
                case "2":
                    _service.AddProduct();
                    break;
                case "3":
                    _service.UpdateProduct();
                    break;
                case "4":
                    _service.DeleteProduct();
                    break;
                case "5":
                    ConsoleHelper.PrintMessage("Avslutar programmet...");
                    Environment.Exit(0); // Avslutar programmet
                    break;
                default:
                    ConsoleHelper.PrintMessage("Ogiltigt val, försök igen.");
                    break;
            }

            ConsoleHelper.PrintMessage("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }
    }
}

