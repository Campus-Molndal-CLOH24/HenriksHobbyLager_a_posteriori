using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Models
{
    internal class ProductUpdater : ConnectionString
    {
        public void UpdateProduct()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE Products SET Name = @Name, Price = @Price, Stock = @Stock, Category = @Category WHERE ID = @ID;";

                    Console.WriteLine("Skriv ID på produkten du vill uppdatera: ");
                    string ID = Console.ReadLine();

                    Console.WriteLine("Namn: ");
                    string Name = Console.ReadLine();

                    Console.WriteLine("Pris: ");
                    double Price = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Lagersaldo: ");
                    int Stock = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Kategori: ");
                    string Category = Console.ReadLine();

                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@Stock", Stock);
                    command.Parameters.AddWithValue("@Category", Category);
                    command.ExecuteNonQuery();

                    Console.WriteLine("Produkt uppdaterad!");
                }
            }
        }
        public static void UpdateOneProduct()
        {
            var UpdateProd = new ProductUpdater();
            UpdateProd.UpdateProduct();
        }
    }
}
