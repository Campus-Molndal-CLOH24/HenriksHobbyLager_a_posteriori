using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Interfaces
{
    public interface IProductService // Console Menu logik (affärslogik). Separerar Affärslogik från Datbaslogik,
{
        IEnumerable<Product> GetAllProducts();
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        IEnumerable<Product> SearchProducts(string searchTerm);
        Product GetProduct(int result);
    }
}