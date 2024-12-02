using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Interfaces
{
    public interface IProductFacade
    {
        IEnumerable<Product> GetAllProducts();
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        IEnumerable<Product> SearchProducts(string searchTerm);
        Product GetProduct(int result);
    }
}