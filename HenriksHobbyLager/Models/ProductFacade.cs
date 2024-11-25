using RefactoringExercise.Interfaces;
using RefactoringExercise.Models;
using System.Linq;

namespace RefactoringExercise.Models
{
    // implementerar affärslogiken för produkter genom IProductFacade
    public class ProductFacade : IProductFacade
    {
        private readonly IRepository<Product> _repository;

        // konstruktor med Dependency Injection för att injicera ett repository
        public ProductFacade(IRepository<Product> repository) => _repository = repository;

        // hämtar alla produkter från lagret
        public IEnumerable<Product> GetAllProducts() => _repository.GetAll();


        // hämtar en specifik produkt baserat på dess unika ID
        public Product GetProduct(int id) => _repository.GetById(id);

        // skapar och lägger till en ny produkt i lagret
        public void CreateProduct(Product product)
        {
            // validerar att produktens namn inte är tomt
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Nu missade Henkemannen att skriva in namnet på produkten.");
            
            // sätter skapat-datumet till nuvarande tidpunkt
            product.Created = DateTime.Now;

            // lägger till produkten i lagret via repository
            _repository.Add(product);
        }

        // uppdaterar en befintlig produkt
        public void UpdateProduct(Product product)
        {
            // hämtar den befintliga produkten baserat på ID
            var existingProduct = _repository.GetById(product.Id);
            if (existingProduct == null)
                throw new ArgumentException("Produkten finns inte i databasen.");

            // uppdaterar produktens egenskaper
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.Category = product.Category;
            existingProduct.LastUpdated = DateTime.Now; // sätter tid för senaste uppdatering
            
            // uppdaterar produkten i lagret via repository
            _repository.Update(existingProduct);
        }

        // tar bort en produkt baserat på dess ID
        public void DeleteProduct(int id)
        {
            // hämtar produkten baserat på ID
            var product = _repository.GetById(id);
            if (product == null)
                throw new ArgumentException("Produkten finns inte i databasen.");

            // tar bort produkten via repository
            _repository.Delete(id);
        } 

        // söker efter produkter baserat på namn eller kategori
        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            // hämtar alla produkter och filtrerar på namn och kategori
            return _repository.GetAll().Where(p => 
                p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }
    }
}