using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Repositories
{
    public class ProductDatabase : IRepository<Product> // ProductDatabase hanterar databaskommunikation och delegerar anrop till den faktiska databasimplementationen (t.ex. SQLite eller MongoDB).
    {
        private readonly IRepository<Product> _database;
        public ProductDatabase(IRepository<Product> database)
        {
            _database = database;
        }
        public void Connect(string connectionString) => _database.Connect(connectionString);
        public IEnumerable<Product> GetAll() => _database.GetAll();
        public Product GetById(int id) => _database.GetById(id);
        public void Add(Product product) => _database.Add(product);
        public void Update(Product product) => _database.Update(product);
        public void Delete(int id) => _database.Delete(id);
        public IEnumerable<Product> Search(string searchTerm) => _database.Search(searchTerm);
    }
}