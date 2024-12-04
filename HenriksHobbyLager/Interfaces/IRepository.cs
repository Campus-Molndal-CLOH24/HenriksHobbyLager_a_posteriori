
namespace HenriksHobbyLager.Interfaces
{
    public interface IRepository<T> // DatabasLogik
    {
        void Connect(string connectionString);
        void Add(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Search(string searchTerm);
        void Update(T entity);
        void Delete(int id);
    }
}