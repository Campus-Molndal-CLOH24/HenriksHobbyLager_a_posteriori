using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager
{
    public class ProductFacade : IProductFacade
    {
        private readonly IRepository<Product> _repository;

        public ProductFacade(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _repository.GetAll();
        }

        public void CreateProduct(Product product)
        {
            _repository.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _repository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            _repository.Delete(id);
        }
        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            return _repository.Search(searchTerm);
        }

        public Product GetProduct(int result)
        {
            return _repository.GetById(result);
        }
    }
}