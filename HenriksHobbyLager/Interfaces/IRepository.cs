namespace HenriksHobbyLager.Interfaces
{
    public interface IRepository<T> // Ansvarar för databaslogik och CRUD-operationer
    {
        // Lägger till en ny entitet i databasen
        Task Add(T entity);

        // Hämtar alla entiteter från databasen
        Task<IEnumerable<T>> GetAll();

        // Hämtar en specifik entitet baserat på ID
        Task<T> GetById(int id);

        // Söker efter entiteter baserat på en sökterm
        Task<IEnumerable<T>> Search(string searchTerm);

        // Uppdaterar en befintlig entitet i databasen
        Task Update(T entity);

        // Tar bort en entitet från databasen baserat på ID
        Task Delete(int id);
    }
}