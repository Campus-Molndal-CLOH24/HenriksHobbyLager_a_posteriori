
namespace HenriksHobbyLager.Utilities
{
    public static class ConfigReader
    {
        public static (string type, string connectionString) ReadConfig(string filePath)
        {
            var configLines = File.ReadAllLines(filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line)) // Ignore empty lines
                .Where(line => !line.Trim().StartsWith("#"))    // Ignore comments
                .Select(line => line.Split(new[] { '=' }, 2))    // Split into at most 2 parts
                .ToDictionary(
                    split => split[0].Trim(),
                    split => split.Length > 1 ? split[1].Trim() : string.Empty // Handle lines without a value
                );
            
            // Get database type
            string dbType = configLines["type"];
            string connectionString = dbType switch
            {
                "SQL" => configLines["sql_connection"],
                "NoSQL" => configLines["mongo_connection"],
                _ => throw new Exception($"Okänd databas typ: {dbType}")
            };
            return (dbType, connectionString);
        }
    }
}
