using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Database
{
    public class MongoDBDatabase : IDatabase
    {
        private IMongoDatabase _database;

        public void Connect(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("HenriksHobbyLager");
            Console.WriteLine("Ansluten till MongoDB");

        }

        public void CreateTable()
        {
            Console.WriteLine("MongoDB behöver ingen tabell, din scrublord!");
        }

        public void AddProduct(Product product)
        {
            var collection = _database.GetCollection<Product>("Products");
            var maxId = collection.AsQueryable().OrderByDescending(p => p.Id).FirstOrDefault()?.Id ?? 0;
            product.Id = maxId + 1;
            collection.InsertOne(product);
            
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var collection = _database.GetCollection<Product>("Products");
            return collection.Find(FilterDefinition<Product>.Empty).ToList();
        }

        public Product GetProductById(int id)
        {
            var collection = _database.GetCollection<Product>("Products");
            return collection.Find(p => p.Id == id).FirstOrDefault();
        }
        public IEnumerable<Product> GetProductByName(string search)
        {
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Regex("Name", new MongoDB.Bson.BsonRegularExpression(search, "i"));
            return collection.Find(filter).ToList();

        }

        public void UpdateProduct(Product product)
        {
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            collection.ReplaceOne(filter, product);
            Console.WriteLine("Produkt uppdaterad!");
        }

        public void DeleteProduct(int id)
        {
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            collection.DeleteOne(filter);
            
        }
    }
}

