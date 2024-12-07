using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Database.Repositories
{
    public class SqliteProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _context;

        public SqliteProductRepository(AppDbContext context)
        {
            // Initialiserar databaskontexten
            _context = context;
        }

        public async Task Add(Product product)
        {
            // Lägger till en ny produkt i SQLite-databasen
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            // Hämtar alla produkter från SQLite-databasen
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            // Hämtar en specifik produkt baserat på dess ID
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> Search(string searchTerm)
        {
            // Söker efter produkter vars namn eller kategori innehåller söktermen
            return await _context.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Category.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task Update(Product product)
        {
            // Uppdaterar en befintlig produkt i SQLite-databasen
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            // Tar bort en produkt från SQLite-databasen baserat på dess ID
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}