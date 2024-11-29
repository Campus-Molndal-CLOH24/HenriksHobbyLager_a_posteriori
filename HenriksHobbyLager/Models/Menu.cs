using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Database;
using RefactoringExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;
using System.Xml.Linq;


namespace HenriksHobbyLager.Models
{
    internal class Menu
    {
        private readonly IDatabase _database;

        public Menu(IDatabase database)
        {
            _database = database;
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
                Console.WriteLine("5. Sök efter produkt");
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
                        DeleteAProduct();
                        break;
                    case "5":
                        SearchProducts();
                        break;
                    case "6":
                        return; 
                    default:
                        Console.WriteLine("Ogiltigt val! Är du säker på att du tryckte på rätt knapp?");
                        break;
                }

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta... (helst inte ESC)");
                Console.ReadKey();
            }
        }

        private void ShowAllProducts()
        {
            var products = _database.GetAllProducts();
            if (products == null || !products.Any())
            {
                Console.WriteLine();
                return;
            }
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id} - {product.Name} - {product.Price} kr - {product.Stock} st - {product.Category} - {product.Created} - {product.LastUpdated}");
            }
        }
        private void AddProduct()
        {
            Console.WriteLine("Lägg till en produkt");

            Console.Write("Namn: "); 
            string name = Console.ReadLine();

            Console.Write("Pris: "); 
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Felaktig inmatning, priset måste vara ett tal!");
                return; 
            }
            Console.Write("Lagersaldo: ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            {
                Console.WriteLine("Felaktig inmatning, lagersaldot måste vara ett heltal!");
                return;
            }
            Console.Write("Kategori: ");
            string category = Console.ReadLine();

            var product = new Product
            {
                Name = name,
                Price = price,
                Stock = stock,
                Category = category
            };
            _database.AddProduct(product);
            Console.WriteLine("Produkt tillagd!");
        }
        private void DeleteAProduct()
        {
            Console.Write("Ange ID på produkten du vill ta bort: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _database.DeleteProduct(id);
                Console.WriteLine("Produkt borttagen!");
            }
            else
            {
                Console.Write("Ogiltigt ID!");
            }

        }

        private void UpdateProduct()
        {
            Console.Write("Skriv ID på produkten du vill uppdatera: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            var product = _database.GetProductById(id);
            if (product == null)
            {
                Console.Write("Produkten finns inte i databasen!");
                return;
            }

            Console.Write("Nytt namn (lämna tomt om du vill behålla det gamla): ");
            string newName = Console.ReadLine();

            Console.Write("Nytt pris (lämna tomt om du vill behålla det gamla): ");
            string newPriceInput = Console.ReadLine();

            Console.Write("Nytt lagersaldo (lämna tomt om du vill behålla det gamla): ");
            string newStockInput = Console.ReadLine();

            Console.Write("Ny kategori (lämna tomt om du vill behålla det gamla): ");
            string newCategory = Console.ReadLine();    

            product.Name = string.IsNullOrEmpty(newName) ? product.Name : newName;
            product.Price = decimal.TryParse(newPriceInput, out decimal newPrice) ? newPrice : product.Price;
            product.Stock = int.TryParse(newStockInput, out int newStock) ? newStock : product.Stock;
            product.Category = string.IsNullOrEmpty(newCategory) ? product.Category : newCategory;

            _database.UpdateProduct(product);
            Console.Write("Produkt uppdaterad!");


        }

        private void SearchProducts()
        {
            Console.Write("Sök efter produkt: ");
            string search = Console.ReadLine();
            var products = _database.GetProductByName(search);
            if (products == null || !products.Any())
            {
                Console.WriteLine("Inga produkter hittades!");
                return;
            }

            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}, Lager: {product.Stock}, Kategori: {product.Category}, Skapad: {product.Created}, Uppdaterad: {product.LastUpdated}");
            }
        }

    }
       
    


        
    }


       