using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Database
{
    public class ConnectionString
    {
        public string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HenriksHobbyLager.db")}"; 

    }
}
