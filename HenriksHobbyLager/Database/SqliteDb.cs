using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.Data.Sqlite;

namespace HenriksHobbyLager.Database
{
    public class SqliteDb : IRepository<Product>
    {
        private string _connectionString;

        public void Connect(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Product product)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Products (Name, Price, Stock, Category, Created) VALUES (@Name, @Price, @Stock, @Category, @Created);";
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Stock", product.Stock);
            command.Parameters.AddWithValue("@Category", product.Category);
            command.Parameters.AddWithValue("@Created", product.Created);
            command.ExecuteNonQuery();
        }

        public IEnumerable<Product> GetAll()
        {
            var products = new List<Product>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = new SqliteCommand("SELECT * FROM Products;", connection);
            using var reader = command.ExecuteReader();
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
                    Updated = reader.IsDBNull(reader.GetOrdinal("Updated")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Updated"))
                });
            }
            return products;
        }

        public Product GetById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = new SqliteCommand("SELECT * FROM Products WHERE ID = @ID;", connection);
            command.Parameters.AddWithValue("@ID", id);
            using var reader = command.ExecuteReader();
            return reader.Read()
                ? new Product
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                    Category = reader.GetString(reader.GetOrdinal("Category")),
                    Created = reader.GetDateTime(reader.GetOrdinal("Created")),
                    Updated = reader.IsDBNull(reader.GetOrdinal("Updated")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Updated"))
                }
                : null;
        }

        public IEnumerable<Product> Search(string searchTerm)
        {
            var products = new List<Product>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = new SqliteCommand("SELECT * FROM Products WHERE Name LIKE @search OR Category LIKE @search;", connection);
            command.Parameters.AddWithValue("@search", $"%{searchTerm}%");
            using var reader = command.ExecuteReader();
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
                    Updated = reader.IsDBNull(reader.GetOrdinal("Updated")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Updated"))
                });
            }
            return products;
        }

        public void Update(Product product)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Products SET Name = @Name, Price = @Price, Stock = @Stock, Category = @Category, Updated = CURRENT_TIMESTAMP WHERE ID = @ID;";
            command.Parameters.AddWithValue("@ID", product.Id);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Stock", product.Stock);
            command.Parameters.AddWithValue("@Category", product.Category);
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = new SqliteCommand("DELETE FROM Products WHERE ID = @ID;", connection);
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();
        }
    }
}