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
                .Select(line => line.Split('='))
                .ToDictionary(split => split[0].Trim(), split => split[1].Trim());

            string dbType = configLines["type"];
            string connectionString = dbType == "SQL"
                ? configLines["sql_connection"]
                : configLines["mongo_connection"];

            return (dbType, connectionString);
        }
    }
}
