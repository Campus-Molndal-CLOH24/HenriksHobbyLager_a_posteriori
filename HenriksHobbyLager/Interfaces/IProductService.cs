using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Interfaces
{
    public interface IProductService // Console Menu logik (affärslogik).
                                     // Separerar Affärslogik från Datbaslogik,
{
        public void CreateProduct(Product product);
        Task<Product> GetProduct(int result);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> SearchProducts(string searchTerm);
        void UpdateProduct(Product product);
        public void DeleteProduct(int id);
    }
}

