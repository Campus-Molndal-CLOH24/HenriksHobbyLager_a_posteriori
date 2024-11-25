namespace RefactoringExercise.Interfaces
{
    // repository gränssnitt CRUD
    public interface IRepository<T>
    {
        // hämtar alla objekt av typen T från datalagret
        IEnumerable<T> GetAll();

        // hämtar ett specifikt objekt av typen T baserat på dess unika ID
        T GetById(int id);

        // lägger till ett nytt objekt av typen T i datalagret
        void Add(T entity);

        // uppdaterar ett befintligt objekt av typen T i datalagret
        void Update(T entity);

        // Tar bort ett objekt baserat på dess unika ID
        void Delete(int id);
        // IEnumerable<T> Search(Func<T, bool> predicate); Ska denna va med?

    }
}