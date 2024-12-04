using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.Data.Sqlite;

namespace HenriksHobbyLager.Database.Repositories
{
    public class SqliteProductRepository : IRepository<Product>
    {
        private string _connectionString;

        public void Connect(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(Product product)
        {
            await using var connection = new SqliteConnection(_connectionString);
            await connection .OpenAsync();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Products (Name, Price, Stock, Category, Created) VALUES (@Name, @Price, @Stock, @Category, @Created);";
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Stock", product.Stock);
            command.Parameters.AddWithValue("@Category", product.Category);
            command.Parameters.AddWithValue("@Created", product.Created);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = new List<Product>();
            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            connection.Open();
            var command = new SqliteCommand("SELECT * FROM Products;", connection);
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
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

        public async Task<Product> GetById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            var command = new SqliteCommand("SELECT * FROM Products WHERE ID = @ID;", connection);
            command.Parameters.AddWithValue("@ID", id);
            using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync()
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

        public async Task<IEnumerable<Product>> Search(string searchTerm)
        {
            var products = new List<Product>();
            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            var command = new SqliteCommand("SELECT * FROM Products WHERE Name LIKE @search OR Category LIKE @search;", connection);
            command.Parameters.AddWithValue("@search", $"%{searchTerm}%");
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
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

        public async Task Update(Product product)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Products SET Name = @Name, Price = @Price, Stock = @Stock, Category = @Category, Updated = CURRENT_TIMESTAMP WHERE ID = @ID;";
            command.Parameters.AddWithValue("@ID", product.Id);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Stock", product.Stock);
            command.Parameters.AddWithValue("@Category", product.Category);
            await command.ExecuteNonQueryAsync();
        }

        public async Task Delete(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            var command = new SqliteCommand("DELETE FROM Products WHERE ID = @ID;", connection);
            command.Parameters.AddWithValue("@ID", id);
            await command.ExecuteNonQueryAsync();
        }
    }
}