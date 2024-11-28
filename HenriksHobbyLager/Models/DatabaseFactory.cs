using HenriksHobbyLager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Models
{
    public static class DatabaseFactory
    {
        public static IDatabase CreateDatabase(string dbType)
        {
            return dbType switch
            {
                "SQL" => new SQLiteDatabase(),
                "NoSQL" => new MongoDBDatabase(),
                _ => throw new ArgumentException("\r\n(╯°□°)╯︵ ┻━┻\r\n Felaktig databastyp faktsiskt!")
            };
        }
    }
}
