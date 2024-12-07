using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Services
{
    public class ProductService : IProductService
    {
        // Mellanhand mellan databasen och resten av applikationen.
        // Ansvarar för att säkerställa att affärslogik är separerad från databaslogik.
        private readonly IRepository<Product> _repository;

        // Dependency Injection för att skicka in ett repository
        public ProductService(IRepository<Product> repository) => _repository = repository;

        // Skapar en ny produkt genom att skicka den till repository
        public void CreateProduct(Product product) => _repository.Add(product);

        // Hämtar alla produkter från repository
        public Task<IEnumerable<Product>> GetAllProducts() => _repository.GetAll();

        // Hämtar en specifik produkt baserat på ID
        public Task<Product> GetProduct(int result) => _repository.GetById(result);

        // Uppdaterar en befintlig produkt
        public void UpdateProduct(Product product) => _repository.Update(product);

        // Söker efter produkter baserat på en sökterm
        public Task<IEnumerable<Product>> SearchProducts(string searchTerm) => _repository.Search(searchTerm);

        // Tar bort en produkt baserat på ID
        public void DeleteProduct(int id) => _repository.Delete(id);
    }
}