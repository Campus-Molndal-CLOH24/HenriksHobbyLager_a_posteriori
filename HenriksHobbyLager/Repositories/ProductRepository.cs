using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Repositories
{
    public class ProductRepository : IRepository<Product>     // implementerar IRepository<Product> för att hantera lagring av Product-objekt
    {
        private readonly List<Product> _products = new(); // Lista för att lagra produkter, Lagrar i databaser nu!

        private int _nextId = 1; // Räknare för ID. Börjar på 1 för att 0 känns så negativt

        public IEnumerable<Product> GetAll() => _products;

        public Product GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void AddProduct(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = GetById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.Category = product.Category;
                existingProduct.LastUpdated = DateTime.Now; // Registrerar den senaste uppdatering
            }
        }

        public void Delete(int id) => _products.RemoveAll(p => p.Id == id);
    }
}