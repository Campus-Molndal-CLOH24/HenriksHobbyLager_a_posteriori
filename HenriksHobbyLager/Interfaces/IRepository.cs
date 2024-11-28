namespace HenriksHobbyLager.Interfaces
{
    // repository gränssnitt CRUD
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();        // hämtar alla produkter i lagret
        T GetById(int id);              // hämtar en specifik produkt baserat på dess ID
        void AddProduct(T entity);      // lägger till en ny produkt i lagret
        void UpdateProduct(T entity);   // uppdaterar en befintlig produkt i lagret
        void Delete(int id);            // tar bort en produkt baserat på dess ID
    }
}