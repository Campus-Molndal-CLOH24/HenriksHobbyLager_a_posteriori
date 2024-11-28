using HenriksHobbyLager.Interfaces;

namespace HenriksHobbyLager.Models
{
    // Denna klass fungerar som en fasad för affärslogiken kring produkter.
    // Den implementerar gränssnittet IProductFacade och använder ett repository för att hantera databasanrop.
    public class ProductFacade : IProductFacade
    {
        private readonly IRepository<Product> _repository; // Dependency för att hantera produktdata.

        // Konstruktor som använder Dependency Injection för att injicera ett repository-objekt.
        public ProductFacade(IRepository<Product> repository)
        {
            _repository = repository;
        }

        // Hämtar alla produkter från repositoryt.
        // Returnerar en lista med alla produkter.
        public IEnumerable<Product> GetAllProducts() => _repository.GetAll();

        // Hämtar en specifik produkt baserat på ett ID.
        public Product GetProduct(int id) => _repository.GetById(id);

        // Skapar och lägger till en ny produkt i repositoryt.
        // Lägger automatiskt till datum för när produkten skapades och senast uppdaterades.
        public void CreateProduct(Product product)
        {
            product.Created = DateTime.Now;
            product.LastUpdated = DateTime.Now;

            // Lägg till produkten i repository
            _repository.AddProduct(product);
        }
        

        // Uppdaterar en befintlig produkt i repositoryt.
        public void UpdateProduct(Product product)
        {
            try
            {
                // Hämtar den befintliga produkten baserat på ID.
                var existingProduct = _repository.GetById(product.Id);
                if (existingProduct == null)
                    throw new ArgumentException("Produkten finns inte i databasen.");

                // Uppdaterar produktens egenskaper.

                UpdateProperties(product, existingProduct);

                // Sparar de uppdaterade ändringarna via repositoryt.
                _repository.UpdateProduct(existingProduct);
            }
            catch (Exception ex)
            {
                // Hanterar eventuella fel och kastar vidare ett tydligare felmeddelande.
                throw new Exception("Ett fel uppstod vid uppdateringen av produkten.", ex);
            }
        }

        private static void UpdateProperties(Product product, Product existingProduct)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.Category = product.Category;
            existingProduct.LastUpdated = DateTime.Now; // Sätter uppdateringsdatum.
        }

        // Söker efter produkter baserat på ett sökord.
        // Jämför sökordet med produktens namn och kategori (skiftlägesokänsligt).
        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            return _repository.GetAll() // Hämtar alla produkter från repositoryt.
                .Where(p =>
                    p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || // Namnmatchning.
                    p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)); // Kategorimatchning.
        }
        // Tar bort en produkt från repositoryt baserat på dess ID.
        public void DeleteProduct(int id)
        {
            try
            {
                // Hämtar produkten för att säkerställa att den existerar innan borttagning.
                var product = _repository.GetById(id);
                if (product == null)
                    throw new ArgumentException("Produkten finns inte i databasen.");

                // Tar bort produkten.
                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                // Hanterar eventuella fel och kastar vidare ett tydligare felmeddelande.
                throw new Exception("Ett fel uppstod vid borttagningen av produkten.", ex);
            }
        }

    }
}