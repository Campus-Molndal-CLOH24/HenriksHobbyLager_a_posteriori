using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Get connection string based on the database type
            string connectionString = dbType == "SQL"
                ? configLines["sql_connection"]
                : configLines["mongo_connection"];

            return (dbType, connectionString);
        }
    }
}
