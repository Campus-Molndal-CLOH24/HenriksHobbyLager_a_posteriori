
using RefactoringExercise.Models;
using RefactoringExercise.Repositories;


namespace RefactoringExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            // Connection string for SQLite database
            string connectionString = "Data Source=products.db";

            // Initialize the database
            DataBaseInit.DataBaseInitialize(connectionString);

            // Create an instance of SqliteProductRepository for database operations
            var repository = new SqliteProductRepository();

            // Create ProductFacade with the repository
            var facade = new ProductFacade(repository);

            // Create ConsoleUi with the facade
            var ui = new ConsoleUi(facade);

            // Start the application
            ui.Run();
        }
    }
} 