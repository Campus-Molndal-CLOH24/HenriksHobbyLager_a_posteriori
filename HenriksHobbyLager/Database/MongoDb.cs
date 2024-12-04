using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace HenriksHobbyLager.Database
{
    public class MongoDb : IRepository<Product>
    {
        private IMongoDatabase _database;

        public void Connect(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("HenriksHobbyLager");
        }

        public void Add(Product product)
        {
            var collection = _database.GetCollection<Product>("Products");
            var maxId = collection.AsQueryable().OrderByDescending(p => p.Id).FirstOrDefault()?.Id ?? 0;
            product.Id = maxId + 1;
            collection.InsertOne(product);
        }

        public IEnumerable<Product> GetAll()
        {
            var collection = _database.GetCollection<Product>("Products");
            return collection.Find(FilterDefinition<Product>.Empty).ToList();
        }

        public Product GetById(int id)
        {
            var collection = _database.GetCollection<Product>("Products");
            return collection.Find(p => p.Id == id).FirstOrDefault();
        }

        public IEnumerable<Product> Search(string searchTerm)
        {
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Regex("Name", new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"));
            return collection.Find(filter).ToList();
        }

        public void Update(Product product)
        {
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            collection.ReplaceOne(filter, product);
        }

        public void Delete(int id)
        {
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            collection.DeleteOne(filter);
        }
    }
}