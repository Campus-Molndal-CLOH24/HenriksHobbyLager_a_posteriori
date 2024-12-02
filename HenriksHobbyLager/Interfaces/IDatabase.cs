using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Interfaces
{
    public interface IDatabase
    {
        void Connect(string connectionString);
        void CreateTable();
        void AddProduct(Product product);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        IEnumerable<Product> GetProductByName(string search);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
