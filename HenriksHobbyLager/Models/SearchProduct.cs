using HenriksHobbyLager.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Models
{
    internal class SearchProduct
    {
        // Sökfunktion - Min stolthet! Söker i både namn och kategori
        private static void SearchProducts()
        {
            Console.Write("Sök (namn eller kategori - versaler spelar ingen roll!): ");
            var searchTerm = Console.ReadLine().ToLower();

            // LINQ igen! Kollar både namn och kategori
            var results = _products.Where(p =>
                p.Name.ToLower().Contains(searchTerm) ||
                p.Category.ToLower().Contains(searchTerm)
            ).ToList();

            if (!results.Any())
            {
                Console.WriteLine("Inga produkter matchade sökningen. Prova med något annat!");
                return;
            }

            foreach (var product in results)
            {
                DisplayProduct(product);
            }
        }
    }
}