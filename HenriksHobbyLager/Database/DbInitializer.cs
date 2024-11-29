using Microsoft.Data.Sqlite;

namespace HenriksHobbyLager.Models
{
    // klass ansvarar för att initiera databasen.
    // Syftet är att se till att nödvändiga tabeller skapas om de inte redan finns.
    public class DbInitializer
    {
        public static void DataBaseInitialize(string connectionString) // Anslutningssträngen används för att koppla upp sig mot SQLite-databasen.

        {
            using (var connection = new SqliteConnection(connectionString)) // Skapar en ny SQLite-anslutning med hjälp av den medföljande anslutningssträngen.

            {
                connection.Open();  // Öppnar anslutningen till databasen.

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