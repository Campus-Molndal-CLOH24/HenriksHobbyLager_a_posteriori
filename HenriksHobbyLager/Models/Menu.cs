using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Database;
using RefactoringExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HenriksHobbyLager.Models
{
    internal class Menu
    {
        public readonly SQLiteRepo<Product> products;

        public Menu()
        {
            products = new SQLiteRepo<Product>("Products");
        }

        public void MainMenu()
        {
            // Huvudloopen - Stäng inte av programmet, då försvinner allt!
            while (true)
            {
                Console.Clear();  // Rensar skärmen så det ser proffsigt ut
                Console.WriteLine("=== Henriks HobbyLager™ 1.0 ===");
                Console.WriteLine("1. Visa alla produkter");
                Console.WriteLine("2. Lägg till produkt");
                Console.WriteLine("3. Uppdatera produkt");
                Console.WriteLine("4. Ta bort produkt");
                Console.WriteLine("5. Sök produkter");
                Console.WriteLine("6. Avsluta");  // Använd inte denna om du vill behålla datan!

                var choice = Console.ReadLine();

                // Switch är tydligen bättre än if-else enligt Google
                switch (choice)
                {
                    case "1":
                        ShowAllProducts();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        UpdateProduct();
                        break;
                    case "4":
                        DeleteProduct();
                        break;
                    case "5":
                        SearchProducts();
                        break;
                    case "6":
                        return;  // OBS! All data försvinner om du väljer denna!
                    default:
                        Console.WriteLine("Ogiltigt val! Är du säker på att du tryckte på rätt knapp?");
                        break;
                }

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta... (helst inte ESC)");
                Console.ReadKey();
            }
        }
        private void AddProduct()
        {
            var product = new Product();
            Console.Write("Namn: ");
            product.Name = Console.ReadLine();
            Console.Write("Pris: ");
            product.Price = decimal.Parse(Console.ReadLine());
            Console.Write("Lagersaldo: ");
            product.Stock = int.Parse(Console.ReadLine());
            Console.Write("Kategori: ");
            product.Category = Console.ReadLine();

            products.Add(product);
            Console.WriteLine("Produkten lades till!");
        }



        private void ShowAllProducts()
        {
            var allProducts = products.GetAll();
            foreach (var product in allProducts)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}, Category: {product.Category}");
            }
        }

        private void UpdateProduct()
        {
            Console.Write("Skriv in produktens ID: ");
            var id = int.Parse(Console.ReadLine());
            var product = products.GetById(id);
            if (product != null)
            {
                Console.Write("Namn: ");
                product.Name = Console.ReadLine();
                Console.Write("Pris: ");
                product.Price = decimal.Parse(Console.ReadLine());
                Console.Write("Lagersaldo: ");
                product.Stock = int.Parse(Console.ReadLine());
                Console.Write("Kategori: ");
                product.Category = Console.ReadLine();

                products.Update(product);
                Console.WriteLine("Produkten uppdaterades!");
            }
            else
            {
                Console.WriteLine("Produkten hittades inte. :(");
            }
        }

        private void DeleteProduct()
        {
            Console.Write("Skriv in produktens ID: ");
            var id = int.Parse(Console.ReadLine());
            products.Delete(id);
            Console.WriteLine("Produkten raderades från databasen!");
        }

        private void SearchProducts()
        {
            Console.Write("Skriv in sökterm: ");
            var searchTerm = Console.ReadLine();
            var searchProducts = products.Search(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                    p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            foreach (var product in searchProducts)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}, Category: {product.Category}");
            }
        }
    }
}
