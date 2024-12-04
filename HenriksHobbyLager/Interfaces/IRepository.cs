
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Interfaces
{
    public interface IRepository<T> // DatabasLogik
    {
        Task Add(T entity);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<IEnumerable<T>> Search(string searchTerm);
        Task Update(T entity);
        Task Delete(int id);
    }
}