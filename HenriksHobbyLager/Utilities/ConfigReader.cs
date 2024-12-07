namespace HenriksHobbyLager.Utilities
{
    public static class ConfigReader
    {
        public static (string type, string connectionString) ReadConfig(string filePath)
        {
            var configLines = File.ReadAllLines(filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line)) // Ignorerar tomma rader
                .Where(line => !line.Trim().StartsWith("#"))    // Ignorerar kommenterade rader
                .Select(line => line.Split(new[] { '=' }, 2))   // Delar varje rad vid '=' i två delar
                .ToDictionary(
                    split => split[0].Trim(), // Nyckeln är texten före '='
                    split => split.Length > 1 ? split[1].Trim() : string.Empty // Värdet är texten efter '='
                );

            // Hämtar databasens typ och anslutningssträng baserat på konfigurationsfilen
            string dbType = configLines["type"];
            string connectionString = dbType switch
            {
                "SQL" => configLines["sql_connection"], // För SQL, använd sql_connection
                "NoSQL" => configLines["mongo_connection"], // För NoSQL, använd mongo_connection
                _ => throw new Exception($"Okänd databas typ: {dbType}") // Kasta ett undantag om typen är okänd
            };
            
            return (dbType, connectionString); // Returnerar databastyp och anslutningssträng som en tuple
        }
    }
}