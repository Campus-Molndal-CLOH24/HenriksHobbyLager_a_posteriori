namespace HenriksHobbyLager.Interfaces
{
    // repository gränssnitt CRUD
    public interface IRepository<T>
    {
        // hämtar alla produkter i lagret
        IEnumerable<T> GetAll();

        // hämtar en specifik produkt baserat på dess ID
        T GetById(int id);

        // lägger till en ny produkt i lagret
        void AddProduct(T entity);

        // uppdaterar en befintlig produkt i lagret
        void UpdateProduct(T entity);

        // tar bort en produkt baserat på dess ID
        void Delete(int id);

    }
}