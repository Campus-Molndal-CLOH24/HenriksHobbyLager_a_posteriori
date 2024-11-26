using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Database
{
    internal class DatabaseQuery : ConnectToDatabase
    {
        public void CreateTable()
        {
            using (var connection = new SqliteConnection("connectionString"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS HenriksDatabas (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name STRING NOT NULL, Price REAL NOT NULL, Stock INTEGER NOT NULL, Category STRING NOT NULL, Created DATETIME DEFAULT CURRENT_TIMESTAMP, Updated DATETIME);";
                    command.CommandText = "CREATE TRIGGER SetUpdatedTimestamp AFTER UPDATE ON HenriksDatabas BEGIN UPDATE HenriksDatabas SET Updated = CURRENT_TIMESTAMP WHERE rowid = NEW.rowid; END;";                
                }
            }
        }

    }
}
