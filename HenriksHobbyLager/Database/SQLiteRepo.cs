using HenriksHobbyLager.Interfaces;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Database
{
    public class SQLiteRepo<T> : ConnectToDatabase, IRepository<T> where T : class, new()
    {
        private readonly string products;
        public SQLiteRepo(string Products)
        {
            products = Products;
        }

        public void Add(T entity)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO {products} (Name, Price, Stock, Category) VALUES (@Name, @Price, @Stock, @Category);";
                command.ExecuteNonQuery();

            }
        }

        public T GetById(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {products} WHERE ID = @ID;";
                command.Parameters.AddWithValue("@ID", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new T();
                    }
                }
            }
            return null;
        }
        public IEnumerable<T> Search(Func<T, bool> predicate)
        {
            var products = new List<T>(); 
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {products};";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new T();
                
                        if (predicate(product))
                        {
                            products.Add(product);
                        }
                }
            }
        }
            return products;
        }
        public IEnumerable<T> GetAll()
        {
            var products = new List<T>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {products};";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new T());
                    }
                }
            }
            return products;
        }
        public void Update(T entity)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"UPDATE {products} SET Name = @Name, Price = @Price, Stock = @Stock, Category = @Category WHERE ID = @ID;";
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM {products} WHERE ID = @ID;";
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }
        
    }
        
}

       
    


