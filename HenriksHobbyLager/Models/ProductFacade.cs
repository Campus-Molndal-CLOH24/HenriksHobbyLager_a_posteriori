using RefactoringExercise.Interfaces;


namespace RefactoringExercise.Models
{
    // Implementerar affärslogiken för produkter
    public class ProductFacade : IProductFacade
    {
        private readonly IRepository<Product> _repository;

        // Konstruktor med Dependency Injection för att injicera ett repository
        public ProductFacade(IRepository<Product> repository)
        {
            _repository = repository;
        }

        // Hämtar alla produkter
        public IEnumerable<Product> GetAllProducts() => _repository.GetAll();

        // Hämtar en specifik produkt baserat på ID
        public Product GetProduct(int id) => _repository.GetById(id);

        // Skapar och lägger till en ny produkt
        public void CreateProduct(Product product)
        {
            try
            {
                product.Created = DateTime.Now;
                product.LastUpdated = DateTime.Now;

                _repository.Add(product);
            }
            catch (Exception ex)
            {
                // Logga eller hantera fel
                throw new Exception("Ett fel uppstod vid skapandet av produkten.", ex);
            }
        }

        // Uppdaterar en befintlig produkt
        public void UpdateProduct(Product product)
        {
            try
            {
                var existingProduct = _repository.GetById(product.Id);
                if (existingProduct == null)
                    throw new ArgumentException("Produkten finns inte i databasen.");

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.Category = product.Category;
                existingProduct.LastUpdated = DateTime.Now;

                _repository.Update(existingProduct);
            }
            catch (Exception ex)
            {
                // Logga eller hantera fel
                throw new Exception("Ett fel uppstod vid uppdateringen av produkten.", ex);
            }
        }

        // Tar bort en produkt
        public void DeleteProduct(int id)
        {
            try
            {
                var product = _repository.GetById(id);
                if (product == null)
                    throw new ArgumentException("Produkten finns inte i databasen.");

                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                // Logga eller hantera fel
                throw new Exception("Ett fel uppstod vid borttagningen av produkten.", ex);
            }
        }

        // Söker efter produkter
        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            return _repository.GetAll()
                .Where(p =>
                    p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }
    }
}