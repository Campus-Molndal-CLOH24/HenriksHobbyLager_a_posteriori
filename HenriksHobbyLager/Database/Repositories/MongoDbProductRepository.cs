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
            // Skapar en anslutning till MongoDB och väljer databasen "HenriksHobbyLager"
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("HenriksHobbyLager");
        }

        public async Task Add(Product product)
        {
            // Lägger till en ny produkt i MongoDB och genererar ett nytt unikt ID
            var collection = _database.GetCollection<Product>("Products");
            var maxId = collection.AsQueryable().OrderByDescending(p => p.Id).FirstOrDefault()?.Id ?? 0;
            product.Id = maxId + 1;
            await collection.InsertOneAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            // Hämtar alla produkter från MongoDB
            var collection = _database.GetCollection<Product>("Products");
            return collection.Find(FilterDefinition<Product>.Empty).ToList();
        }

        public async Task<Product> GetById(int id)
        {
            // Hämtar en specifik produkt baserat på dess ID
            var collection = _database.GetCollection<Product>("Products");
            return collection.Find(p => p.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<Product>> Search(string searchTerm)
        {
            // Söker efter produkter vars namn eller kategori matchar söktermen (case-insensitive)
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Regex("Name", new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")) |
                         Builders<Product>.Filter.Regex("Category", new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"));
            return collection.Find(filter).ToList();
        }

        public async Task Update(Product product)
        {
            // Uppdaterar en befintlig produkt i MongoDB baserat på dess ID
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            await collection.ReplaceOneAsync(filter, product);
        }

        public async Task Delete(int id)
        {
            // Tar bort en produkt från MongoDB baserat på dess ID
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            await collection.DeleteOneAsync(filter);
        }
    }
}