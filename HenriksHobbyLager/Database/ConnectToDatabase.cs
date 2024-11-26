using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Database
{
    public class ConnectToDatabase : ConnectionString
    {
        public void connectToDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Uppkopplad till Henriks databas!");
            }
        }
    }
}
         

