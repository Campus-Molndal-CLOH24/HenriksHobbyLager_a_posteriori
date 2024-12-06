using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Services
{
    public class ProductService : IProductService // mellanhand mellan databasen och resten av applikationen,  säkerställer att andra delar av applikationen inte behöver hantera databaslogik direkt.
    {
        private readonly IRepository<Product> _repository;
        public ProductService(IRepository<Product> repository) => _repository = repository;
        public Task<IEnumerable<Product>> GetAllProducts() => _repository.GetAll();
        public void CreateProduct(Product product) => _repository.Add(product);
        public void UpdateProduct(Product product) => _repository.Update(product);
        public void DeleteProduct(int id) => _repository.Delete(id);
        public Task<IEnumerable<Product>> SearchProducts(string searchTerm) => _repository.Search(searchTerm);
        public Task<Product> GetProduct(int result) => _repository.GetById(result);
    }
}