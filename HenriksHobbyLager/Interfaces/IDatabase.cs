using HenriksHobbyLager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Interfaces
{
    public interface IDatabase
    {
        void Connect(string connectionString);
        void CreateTable();
        void AddProduct(Product product);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
