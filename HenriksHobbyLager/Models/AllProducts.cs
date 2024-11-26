using HenriksHobbyLager.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Models
{
    internal class AllProducts
    {
        // Visar alla produkter som finns i "databasen"
        private static void ShowAllProducts()
        {
            // Kollar om det finns några produkter alls
            // !_products.Any() låter mer proffsigt än _products.Count == 0
            if (!_products.Any())
            {
                Console.WriteLine("Inga produkter finns i lagret. Dags att shoppa grossist!");
                return;
            }

            foreach (var product in _products)
            {
                DisplayProduct(product);
            }
        }
    }
}
