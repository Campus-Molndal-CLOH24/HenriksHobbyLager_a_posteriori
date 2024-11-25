using RefactoringExercise.Models;

namespace RefactoringExercise.Interfaces
{
    // produktfacaden som hanterar affärslogiken för produkter
    public interface IProductFacade
    {
        // hämtar alla produkter som finns i systemet
        IEnumerable<Product> GetAllProducts();

        // hämtar en specifik produkt baserat på dess unika ID
        Product GetProduct(int id);

        // skapar och lägger till en ny produkt i systemet
        void CreateProduct(Product product);

        // uppdaterar en befintlig produkts egenskaper
        void UpdateProduct(Product product);

        // tar bort en produkt baserat på dess ID
        void DeleteProduct(int id);

        // söker efter produkter baserat på ett sökterm (tex namn eller kategori)
        IEnumerable<Product> SearchProducts(string searchTerm);
    }
}