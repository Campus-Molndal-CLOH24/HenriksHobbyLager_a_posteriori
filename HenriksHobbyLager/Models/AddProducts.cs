using HenriksHobbyLager.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Models
{
    internal class AddProducts
    {
        // Lägger till en ny produkt i systemet
        private static void AddProduct()
        {
            Console.WriteLine("=== Lägg till ny produkt ===");

            Console.Write("Namn: ");
            var name = Console.ReadLine();

            Console.Write("Pris: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Ogiltigt pris! Använd punkt istället för komma (lärde mig den hårda vägen)");
                return;
            }

            Console.Write("Antal i lager: ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            {
                Console.WriteLine("Ogiltig lagermängd! Hela tal endast (kan inte sälja halva helikoptrar)");
                return;
            }

            Console.Write("Kategori: ");
            var category = Console.ReadLine();

            // Skapar produkten - Id räknas upp automatiskt så vi slipper hålla reda på det
            var product = new Product
            {
                Id = _nextId++,  // Plussar på direkt, smart va?
                Name = name,
                Price = price,
                Stock = stock,
                Category = category,
                Created = DateTime.Now  // Automatiskt datum, smooth!
            };

            _products.Add(product);
            Console.WriteLine("Produkt tillagd! Glöm inte att hålla datorn igång!");
        }
    }
}
