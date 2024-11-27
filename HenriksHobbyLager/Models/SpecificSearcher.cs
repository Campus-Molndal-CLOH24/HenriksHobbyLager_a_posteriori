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
    internal class SpecificSearcher : ConnectionString
    {
        public void SearchSpecific()
        {
            Console.WriteLine("Sök efter produkt: ");
            string search = Console.ReadLine();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT * FROM Products WHERE Name LIKE @search;", connection))
                {
                    command.Parameters.AddWithValue("@search", "%" + search + "%");
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Sökresultat: ");

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
        }
        public static void SearchSpecificProduct()
        {
            var SearchSpec = new SpecificSearcher();
            SearchSpec.SearchSpecific();
        }
    }
}
