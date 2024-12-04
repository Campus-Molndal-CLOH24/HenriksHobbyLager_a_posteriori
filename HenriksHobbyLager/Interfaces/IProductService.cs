using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Interfaces
{
    public interface IProductService // Console Menu logik (affärslogik). Separerar Affärslogik från Datbaslogik,
{
        Task<IEnumerable<Product>> GetAllProducts();
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        Task<IEnumerable<Product>> SearchProducts(string searchTerm);
        Task<Product> GetProduct(int result);
    }
}