using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;

namespace HenriksHobbyLager.Database.Repositories
{
    public class MongoDbProductRepository : IRepository<Product>
    {
        private IMongoDatabase _database;

        public void Connect(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("HenriksHobbyLager");
        }

        public async Task Add(Product product)
        {
            var collection = _database.GetCollection<Product>("Products");
            var maxId = collection.AsQueryable().OrderByDescending(p => p.Id).FirstOrDefault()?.Id ?? 0;
            product.Id = maxId + 1;
            await collection.InsertOneAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var collection = _database.GetCollection<Product>("Products");
            return collection.Find(FilterDefinition<Product>.Empty).ToList();
        }

        public async Task<Product> GetById(int id)
        {
            var collection = _database.GetCollection<Product>("Products");
            return collection.Find(p => p.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<Product>> Search(string searchTerm)
        {
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Regex("Name", new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"));
            return collection.Find(filter).ToList();
        }

        public async Task Update(Product product)
        {
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            await collection.ReplaceOneAsync(filter, product);
        }

        public async Task Delete(int id)
        {
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            await collection.DeleteOneAsync(filter);
        }
    }
}