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
    internal class DeleteProduct : ConnectionString
    {
        public void DeleteProd()
        {
            Console.WriteLine("Skriv ID på produkten du vill ta bort: ");
            string delete = Console.ReadLine();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand())
                {   
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM Products WHERE ID LIKE @delete;";
                    command.Parameters.AddWithValue("@delete", "%" + delete + "%");
                    command.ExecuteNonQuery();

                    Console.WriteLine("Produkt borttagen!");
                }
            }
        }
        public static void DeleteOneProduct()
        {
            var DeleteProd = new DeleteProduct();
            DeleteProd.DeleteProd();
        }
    }
}
