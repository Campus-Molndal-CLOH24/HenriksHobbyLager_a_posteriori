using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.Data.Sqlite;

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
                command.CommandText =
                    "CREATE TABLE IF NOT EXISTS Products (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name STRING NOT NULL, Price REAL NOT NULL, Stock INTEGER NOT NULL, Category STRING NOT NULL, Created DATETIME DEFAULT CURRENT_TIMESTAMP, Updated DATETIME);";
                command.ExecuteNonQuery();
                Console.WriteLine("Tabell skapad!");
            }
        }


        public void AddProduct(Product product) // Lägger till en produkt i databasen
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    "INSERT INTO Products (Name, Price, Stock, Category Created) VALUES (@Name, @Price, @Stock, @Category @Created);";
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.Parameters.AddWithValue("@Created", product.Created);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Product> GetAllProducts() // Hämtar alla produkter från databasen
        {
            var products = new List<Product>();
            using (var connection = new SqliteConnection(_connectionString)) // Skapar en anslutning till databasen
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

                        products.Add(new Product
                        {
                            Id = id,
                            Name = name,
                            Price = (decimal)price,
                            Stock = stock,
                            Category = category,
                            Created = created,
                            Updated = updated
                        });
                    }
                }
            }

            foreach (var product in products)
            {
                Console.WriteLine(
                    $"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}, Lager: {product.Stock}, Kategori: {product.Category}, Skapad: {product.Created}, Uppdaterad: {product.Updated}");
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
                                Updated = reader.IsDBNull(reader.GetOrdinal("Updated"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("Updated"))
                            };
                        }
                    }
                }
            }

            return null;
        }

        public IEnumerable<Product> GetProductByName(string name)
        {
            {
                var products = new List<Product>();

                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    using (var command =
                           new SqliteCommand("SELECT * FROM Products WHERE Name LIKE @search OR Category LIKE @search;",
                               connection))

                    {
                        command.Parameters.AddWithValue("@search", "%" + name + "%");
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new Product
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                                    Category = reader.GetString(reader.GetOrdinal("Category")),
                                    Created = reader.GetDateTime(reader.GetOrdinal("Created")),
                                    Updated = reader.IsDBNull(reader.GetOrdinal("Updated"))
                                        ? (DateTime?)null
                                        : reader.GetDateTime(reader.GetOrdinal("Updated"))
                                });
                            }
                        }
                    }
                }

                return products;
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
                    command.CommandText =
                        "UPDATE Products SET Name = @Name, Price = @Price, Stock = @Stock, Category = @Category, Updated = CURRENT_TIMESTAMP WHERE ID = @ID;";
                    command.Parameters.AddWithValue("@ID", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Stock", product.Stock);
                    command.Parameters.AddWithValue("@Category", product.Category);
                    command.ExecuteNonQuery();
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
                }
            }
        }
    }
}