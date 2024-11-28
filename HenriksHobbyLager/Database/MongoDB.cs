﻿using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Database
{
    public class MongoDB : IDatabase
    {
        private IMongoDatabase _database;

        public void Connect(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("HenriksHobbyLager");
            Console.WriteLine("Ansluten till MongoDB");

        }

        public void CreateTable()
        {
            Console.WriteLine("MongoDB behöver ingen tabell, din scrublord!");
        }

        public void AddProduct(Product product)
        {
            var collection = _database.GetCollection<Product>("Products");
            collection.InsertOne(product);
            Console.WriteLine("Produkt tillagd!");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var collection = _database.GetCollection<Product>("Products");
            return collection.Find(FilterDefinition<Product>.Empty).ToList();
        }

        public Product GetProductById(int id)
        {
            var collection = _database.GetCollection<Product>("Products");
            return collection.Find(p => p.Id == id).FirstOrDefault();
        }

        public void UpdateProduct(Product product)
        {
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            collection.ReplaceOne(filter, product);
            Console.WriteLine("Produkt uppdaterad!");
        }

        public void DeleteProduct(int id)
        {
            var collection = _database.GetCollection<Product>("Products");
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            collection.DeleteOne(filter);
            Console.WriteLine("Produkt borttagen!");    
        }
    }
}
