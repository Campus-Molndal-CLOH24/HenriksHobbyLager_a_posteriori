using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.Data.Sqlite;

namespace HenriksHobbyLager.Repositories
{
    public class SqliteProductRepository : IRepository<Product>
    {
        private readonly string _connectionString;

        public SqliteProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private SqliteConnection GetOpenConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public void AddProduct(Product product)
        {
            using (var connection = GetOpenConnection())
            {
                var command = connection.CreateCommand(); // Skapar ett SQL-kommando för att infoga en ny rad i Product-tabellen.

                command.CommandText = @"INSERT INTO Product (Name, Price, Stock, Category, Created, LastUpdated)
                                        VALUES (@Name, @Price, @Stock, @Category, @Created, @LastUpdated)";

                // Lägger till parametrar för att skydda mot SQL-injektion.
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.Parameters.AddWithValue("@Created", product.Created);
                command.Parameters.AddWithValue("@LastUpdated", product.LastUpdated);

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = GetOpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Product WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id); // Skyddar mot SQL-injektion.

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            var products = new List<Product>(); // Skapar en lista för att lagra produkterna.

            using (var connection = GetOpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Product";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetInt32(0), // Hämtar kolumnvärden baserat på index.
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Stock = reader.GetInt32(3),
                            Category = reader.GetString(4),
                            Created = reader.GetDateTime(5),
                            LastUpdated = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
                        });
                    }
                }
            }

            return products;
        }

        public Product GetById(int id)
        {
            using (var connection = GetOpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Product WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) 
                    {
                        return new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Stock = reader.GetInt32(3),
                            Category = reader.GetString(4),
                            Created = reader.GetDateTime(5),
                            LastUpdated = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
                        };
                    }
                }
            }

            return null;
        }

        public void UpdateProduct(Product product)
        {
            using (var connection = GetOpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"UPDATE Product SET Name = @Name, Price = @Price, Stock = @Stock,
                                        Category = @Category, LastUpdated = @LastUpdated WHERE Id = @Id";

                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.Parameters.AddWithValue("@LastUpdated", product.LastUpdated);
                command.Parameters.AddWithValue("@Id", product.Id);

                command.ExecuteNonQuery();
            }
        }
    }
}