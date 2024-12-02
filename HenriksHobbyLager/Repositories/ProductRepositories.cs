using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager_a_posteriori.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HenriksHobbyLager.Repositories
{
    public class ProductRepository: IRepository// Hanterar datalager, ändringar av pris, antal, kategori och namn
    {
        private readonly List<Product> _products = new();
        private int _nextId = 1;

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            product.Id = _nextId++;
            product.Created = DateTime.Now;
            _products.Add(product);
        }

        public void Update(Product updatedProduct)
        {
            var product = GetById(updatedProduct.Id);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
                product.Stock = updatedProduct.Stock;
                product.Category = updatedProduct.Category;
             
            }
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }       

}

    
 
