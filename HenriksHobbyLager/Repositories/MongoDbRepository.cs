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

        public void AddProduct(Product product)
        {
            try
            {
                _products.InsertOne(product);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the product.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _products.DeleteOne(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the product.", ex);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            try
            {
                return _products.Find(p => true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }

        public Product GetById(int id)
        {
            try
            {
                return _products.Find(p => p.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the product.", ex);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                _products.ReplaceOne(p => p.Id == product.Id, product); // ReplaceOne avnänds för uppdatera projektet. 
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the product.", ex);
            }
        }
    }
}