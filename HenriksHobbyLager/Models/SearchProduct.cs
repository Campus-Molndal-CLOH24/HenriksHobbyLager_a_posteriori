using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Database;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Models
{
    internal class SearchProduct
    {
        // Sökfunktion - Min stolthet! Söker i både namn och kategori
        private static void SearchProducts()
        {
            public IEnumerable<T> GetAll()
            {
                var results = new List<T>();

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    var query = $"SELECT * FROM {_tableName};";
                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(MapReaderToEntity(reader));
                        }
                    }
                }

                return results;
            }
        }
    }
}