using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.Data.Sqlite;
// Importerar biblioteket för att arbeta med SQLite.
// Inkluderar gränssnittet som definierar repository-metoder.

// Inkluderar modellen `Product`.

namespace HenriksHobbyLager.Repositories
{
    // Implementerar IRepository för att hantera Product-data via SQLite.
    public class SqliteProductRepository : IRepository<Product>
    {
        // Anger anslutningssträngen för SQLite-databasen.
        // Här används en lokal fil 'products.db'.
        private readonly string _connectionString;

        public SqliteProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Lägger till en ny produkt i databasen.
        public void AddProduct(Product product)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open(); // Öppnar anslutningen till databasen.

                // Skapar ett SQL-kommando för att infoga en ny rad i Product-tabellen.
                var command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO Product (Name, Price, Stock, Category, Created, LastUpdated)
                                        VALUES (@Name, @Price, @Stock, @Category, @Created, @LastUpdated)";

                // Lägger till parametrar för att skydda mot SQL-injektion.
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.Parameters.AddWithValue("@Created", product.Created);
                command.Parameters.AddWithValue("@LastUpdated", product.LastUpdated);

                // Exekverar kommandot och infogar produkten.
                command.ExecuteNonQuery();
            }
        }

        // Tar bort en produkt från databasen baserat på ID.
        public void Delete(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open(); // Öppnar anslutningen.

                // Skapar ett SQL-kommando för att radera en produkt.
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Product WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id); // Skyddar mot SQL-injektion.

                // Exekverar kommandot och tar bort produkten.
                command.ExecuteNonQuery();
            }
        }

        // Hämtar alla produkter från databasen.
        public IEnumerable<Product> GetAll()
        {
            var products = new List<Product>(); // Skapar en lista för att lagra produkterna.

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                // Skapar ett SQL-kommando för att hämta alla rader från Product-tabellen.
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Product";

                // Exekverar kommandot och läser resultaten.
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) // Itererar över varje rad.
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetInt32(0), // Hämtar kolumnvärden baserat på index.
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Stock = reader.GetInt32(3),
                            Category = reader.GetString(4),
                            Created = reader.GetDateTime(5),
                            LastUpdated = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
                        });
                    }
                }
            }

            return products; // Returnerar listan med produkter.
        }

        // Hämtar en specifik produkt baserat på ID.
        public Product GetById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                // Skapar ett SQL-kommando för att hämta en produkt baserat på ID.
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Product WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                // Exekverar kommandot och läser resultatet.
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) // Kontrollera om en rad finns.
                    {
                        return new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Stock = reader.GetInt32(3),
                            Category = reader.GetString(4),
                            Created = reader.GetDateTime(5),
                            LastUpdated = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
                        };
                    }
                }
            }

            return null; // Returnerar null om produkten inte hittas.
        }

        // Uppdaterar en befintlig produkt i databasen.
        public void UpdateProduct(Product product)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                // Skapar ett SQL-kommando för att uppdatera produktens data.
                var command = connection.CreateCommand();
                command.CommandText = @"UPDATE Product SET Name = @Name, Price = @Price, Stock = @Stock,
                                        Category = @Category, LastUpdated = @LastUpdated WHERE Id = @Id";

                // Lägger till parametrar för att skydda mot SQL-injektion.
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.Parameters.AddWithValue("@LastUpdated", product.LastUpdated);
                command.Parameters.AddWithValue("@Id", product.Id);

                // Exekverar kommandot och uppdaterar produkten.
                command.ExecuteNonQuery();
            }
        }
    }
}