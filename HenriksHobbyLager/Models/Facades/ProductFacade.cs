using HenriksHobbyLager.Interfaces;

namespace HenriksHobbyLager.Models
{
    public class ProductFacade : IProductFacade // Denna klass fungerar som en fasad för affärslogiken kring produkter.
    {
        private readonly IRepository<Product> _repository; // Dependency för att hantera produktdata.

        public ProductFacade(IRepository<Product> repository)
        {
            _repository = repository; // dependency injection
        }

        public IEnumerable<Product> GetAllProducts() => _repository.GetAll();

        public Product GetProduct(int id) => _repository.GetById(id); // Hämtar en specifik produkt baserat på ett ID.

        public void CreateProduct(Product product) // Skapar en ny produkt i repositoryt.
        {
            product.Created = DateTime.Now;
            product.LastUpdated = DateTime.Now; // Sätter skapelse- och uppdateringsdatum.
            _repository.AddProduct(product); // Lägg till produkten i repository
        }


        public void UpdateProduct(Product product)
        {
            var existingProduct = _repository.GetById(product.Id);
            if (existingProduct == null)
                throw new ArgumentException("Produkten finns inte i databasen.");
            
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.Category = product.Category;
                existingProduct.LastUpdated = DateTime.Now;
                _repository.UpdateProduct(existingProduct);
            
        }

        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            return _repository.GetAll() // Hämtar alla produkter från repositoryt.
                .Where(p =>
                    p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || // Namnmatchning ELLER
                    p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)); // Kategorimatchning.
        }

        public void DeleteProduct(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
                throw new ArgumentException("Produkten finns inte i databasen.");

            _repository.Delete(id);
        }
    }
}