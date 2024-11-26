using HenriksHobbyLager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Database
{
    public class SQLiteRepo<T> : ConnectToDatabase, IRepository<T> where T : class, new() 
    {
        private readonly string _tableName;
        public SQLiteRepo(string tableName)
        {
            _tableName = tableName;
        }

    }
}

