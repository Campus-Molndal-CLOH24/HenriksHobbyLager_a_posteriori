using Microsoft.Data.Sqlite;

namespace RefactoringExercise.Models
{
    public class DataBaseInit
    {
        public static void DataBaseInitialize(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Product (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Price REAL NOT NULL,
                    Stock INTEGER NOT NULL,
                    Category TEXT NOT NULL,
                    Created TEXT NOT NULL,
                    LastUpdated TEXT
                )";
                command.ExecuteNonQuery();
            }
        }
    }
}