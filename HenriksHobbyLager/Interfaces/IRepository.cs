

using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        IEnumerable<T> Search(string searchTerm); // Lägg till sökmetoden
        Product GetById(int result);
    }
    
}
