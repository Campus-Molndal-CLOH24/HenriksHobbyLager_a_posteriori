using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;


namespace HenriksHobbyLager.Database
{
    public class AppDbContext : DbContext // Entityframework Core integration
    {
        public AppDbContext(DbSet<Product> products)
        {
            Products = products;
        }

        public DbSet<Product> Products { get; }
        public IEnumerable<Product> GetAllProducts() => Products.ToList();
        public Product GetProductById(int id) => Products.Find(id);
        public IEnumerable<Product> GetProductByName(string searchTerm) => Products.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
        public void AddProduct(Product product)
        {
            Products.Add(product);
            SaveChanges(); // Från EntityFramework
        }

        public void UpdateProduct(Product product)
        {
            Products.Update(product);
            SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = Products.Find(id);
            if (product != null)
            {
                Products.Remove(product);
                SaveChanges();
            }
        }
    }
}