using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.Data.Sqlite;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HenriksHobbyLager.Database
{
    public class SQLiteDatabase : IDatabase
    {
        private string _connectionString;
        public void Connect(string connectionString)
        {
            _connectionString = connectionString;
            Console.WriteLine("Ansluten till SQL-databasen");
        }

        public void CreateTable()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Products (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name STRING NOT NULL, Price REAL NOT NULL, Stock INTEGER NOT NULL, Category STRING NOT NULL, Created DATETIME DEFAULT CURRENT_TIMESTAMP, Updated DATETIME);";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TRIGGER SetUpdatedTimestamp AFTER UPDATE ON Products BEGIN UPDATE Products SET Updated = CURRENT_TIMESTAMP WHERE rowid = NEW.rowid; END;";
                command.ExecuteNonQuery();
                Console.WriteLine("Tabell skapad!");
            }
        }


        public void AddProduct(Product product)
        {


            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Products (Name, Price, Stock, Category) VALUES (@Name, @Price, @Stock, @Category);";
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.ExecuteNonQuery();
                Console.WriteLine("Produkt tillagd!");
            }
        }
        public IEnumerable<Product> GetAllProducts()
        {
            using (var connection = new SqliteConnection(_connectionString))
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
            return null;
        }
        public Product GetProductById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT * FROM Products WHERE ID = @ID;", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Product
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                                Category = reader.GetString(reader.GetOrdinal("Category")),
                                Created = reader.GetDateTime(reader.GetOrdinal("Created")),
                                LastUpdated = reader.IsDBNull(reader.GetOrdinal("Updated"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("Updated"))
                            };
                        }
                    }
                }
            }
            return null;
        }
        public Product GetProductByName(string name)
        {
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqliteCommand("SELECT * FROM Products WHERE Name = @Name;", connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Product
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                                    Category = reader.GetString(reader.GetOrdinal("Category")),
                                    Created = reader.GetDateTime(reader.GetOrdinal("Created")),
                                    LastUpdated = reader.IsDBNull(reader.GetOrdinal("Updated"))
                                        ? (DateTime?)null
                                        : reader.GetDateTime(reader.GetOrdinal("Updated"))
                                };
                            }
                        }
                    }
                }
                return null;
            }
        }
            public void UpdateProduct(Product product)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE Products SET Name = @Name, Price = @Price, Stock = @Stock, Category = @Category WHERE ID = @ID;";
                    command.Parameters.AddWithValue("@ID", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Stock", product.Stock);
                    command.Parameters.AddWithValue("@Category", product.Category);
                    command.ExecuteNonQuery();

                    Console.WriteLine("Produkt uppdaterad!");
                }
            }
        }

        public void DeleteProduct(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM Products WHERE ID = @ID;";
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();

                    Console.WriteLine("Produkt borttagen!");
                }
            }
        }
    }
}
