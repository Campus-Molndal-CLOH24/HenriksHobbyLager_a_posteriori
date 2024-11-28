using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Repositories
{
    // implementerar IRepository<T> för att hantera lagring av Product-objekt
    public class ProductRepository : IRepository<Product>
    {
        // Min fantastiska databas! Fungerar perfekt så länge datorn är igång
        // UPDATE: Nu lagrar den i minnet istället för på hårddisken
        private readonly List<Product> _products = new();
        
        // Räknare för ID. Börjar på 1 för att 0 känns så negativt
        private int _nextId = 1;

        // hämtar alla produkter i lagret
        public IEnumerable<Product> GetAll() => _products;

        // hämtar en specifik produkt baserat på dess ID
        public Product GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        // lägger till en ny produkt i lagret
        public void AddProduct(Product product)
        {
            // tilldelar ett nytt ID och lägger till produkten i listan
            product.Id = _nextId++;
            _products.Add(product);
        }

        // uppdaterar en befintlig produkt i lagret
        public void UpdateProduct(Product product)
        {
            // hitta produkten som ska uppdateras
            var existingProduct = GetById(product.Id);
            if (existingProduct != null)
            {
                // uppdatera egenskaperna för den befintliga produkten
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.Category = product.Category;
                existingProduct.LastUpdated = DateTime.Now; // Registrerar den senaste uppdatering
            }
        }

        // tar bort en produkt baserat på dess ID
        public void Delete(int id) => _products.RemoveAll(p => p.Id == id);

    }
}