using Microsoft.Data.Sqlite;

namespace HenriksHobbyLager.Models
{
    // klass ansvarar för att initiera databasen.
    // Syftet är att se till att nödvändiga tabeller skapas om de inte redan finns.
    public class DataBaseInit
    {
        // metod tar en anslutningssträng (connectionString) som parameter.
        // Anslutningssträngen används för att koppla upp sig mot SQLite-databasen.
        public static void DataBaseInitialize(string connectionString)
        {
            // Skapar en ny SQLite-anslutning med hjälp av den medföljande anslutningssträngen.
            using (var connection = new SqliteConnection(connectionString))
            {
                // Öppnar anslutningen till databasen.
                connection.Open();

                // Skapar ett kommando som ska skickas till databasen.
                var command = connection.CreateCommand();
                // SQL-kommandot som ska exekveras. Här används "CREATE TABLE IF NOT EXISTS",
                // vilket gör att tabellen bara skapas om den inte redan finns.
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
            // När "using"-blocket (rad 14) avslutas stängs anslutningen automatiskt,
            // vilket är viktigt för att frigöra resurser och undvika låsningar.
        }
    }
}