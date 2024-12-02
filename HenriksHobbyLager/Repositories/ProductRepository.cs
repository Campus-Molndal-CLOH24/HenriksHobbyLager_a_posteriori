using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public void Add(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Product> Search(string searchTerm)
        {
            return _dbContext.Products
                .Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()) || 
                            p.Category.ToLower().Contains(searchTerm.ToLower()))
                .ToList();
        }
    }
}