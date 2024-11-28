using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;

namespace HenriksHobbyLager.Repositories
{
    public class MongoDBProductRepository : IRepository<Product>
    {
        private readonly IMongoCollection<Product> _products;

        public MongoDBProductRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public void AddProduct(Product product) =>_products.InsertOne(product);

        public void Delete(int id) =>_products.DeleteOne(p => p.Id == id);

        public IEnumerable<Product> GetAll() =>  _products.Find(p => true).ToList();
        
        public Product GetById(int id) =>  _products.Find(p => p.Id == id).FirstOrDefault();
        
        public void UpdateProduct(Product product) => _products.ReplaceOne(p => p.Id == product.Id, product); // ReplaceOne avnänds för uppdatera projektet. 
    }
}

