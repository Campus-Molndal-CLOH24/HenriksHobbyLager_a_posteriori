using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography.X509Certificates;

namespace HenriksHobbyLager.Models
{
    internal class SearchAllProducts : ConnectionString
    { 
            public void ShowAllProd()
            {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT * FROM Products;", connection))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Alla produkter: ");

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string name = reader.GetString(reader.GetOrdinal("Name"));
                        double price = reader.GetDouble(reader.GetOrdinal("Price"));
                        int stock = reader.GetInt32(reader.GetOrdinal("Stock"));
                        string category = reader.GetString(reader.GetOrdinal("Category"));
                        DateTime created = reader.GetDateTime(reader.GetOrdinal("Created"));
                        DateTime? updated = reader.IsDBNull(reader.GetOrdinal("Updated"))
                            ? (DateTime?)null 
                            : reader.GetDateTime(reader.GetOrdinal("Updated"));

                        Console.WriteLine($"ID: {id}, Namn: {name}, Pris: {price}, Lager: {stock}, Kategori: {category}, Skapad: {created}, Uppdaterad: {updated}");

                    }
                }
            }

        }
        public static void SearchAll()
        {
            var SearchAll = new SearchAllProducts();
            SearchAll.ShowAllProd();

        }
    }
}
