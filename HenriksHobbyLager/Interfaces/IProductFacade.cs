using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Interfaces
{
    public interface IProductFacade             // produktfacaden som hanterar affärslogiken för produkter
    {
        IEnumerable<Product> GetAllProducts(); // hämtar alla produkter som finns i systemet
        Product GetProduct(int id);             // hämtar en specifik produkt baserat på dess unika ID
        void CreateProduct(Product product);    // skapar och lägger till en ny produkt i systemet
        void UpdateProduct(Product product);    // uppdaterar en befintlig produkts egenskaper
        void DeleteProduct(int id);                 // tar bort en produkt baserat på dess ID
        IEnumerable<Product> SearchProducts(string searchTerm); // söker efter produkter baserat på ett sökterm (tex namn eller kategori)
    }
}