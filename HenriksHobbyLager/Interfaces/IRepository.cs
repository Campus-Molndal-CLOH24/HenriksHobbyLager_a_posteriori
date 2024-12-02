using System.Collections.Generic;

namespace HenriksHobbyLager.Interfaces
{
    public interface IRepository<T>//better for several tables. 
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
