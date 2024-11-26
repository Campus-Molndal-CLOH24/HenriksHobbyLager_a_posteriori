using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Database;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Models
{
    public class CreateEntry : ConnectionString
    {
        public static void CreateProduct()
        {
            Console.WriteLine("Namn: ");
            string Name = Console.ReadLine();

            Console.WriteLine("Pris: ");
            double Price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Lagersaldo: ");
            int Stock = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Kategori: ");
            string Category = Console.ReadLine();

            var CreateProduct = new CreateEntry();
            CreateProduct.AddProduct(Name, Price, Stock, Category);

            Console.WriteLine("Produkt tillagd!");


        }
        public void AddProduct(string Name, double Price, int Stock, string Category)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO Products (Name, Price, Stock, Category) VALUES (@Name, @Price, @Stock, @Category);";
                    

                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@Stock", Stock);
                    command.Parameters.AddWithValue("@Category", Category);
                    command.ExecuteNonQuery();

                }
            }
        }
    
       
    }
}
