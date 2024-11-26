using HenriksHobbyLager.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Models
{
    internal class DeleteProducts
    {
        // Ta bort en produkt (använd med försiktighet!)
        private static void DeleteProduct()
        {
            Console.Write("Ange produkt-ID att ta bort (dubbel-check att det är rätt, går inte att ångra!): ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID! Bara siffror är tillåtna här.");
                return;
            }

            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Produkt hittades inte! Puh, inget blev raderat av misstag!");
                return;
            }

            _products.Remove(product);
            Console.WriteLine("Produkt borttagen! (Hoppas det var meningen)");
        }
    }
}
