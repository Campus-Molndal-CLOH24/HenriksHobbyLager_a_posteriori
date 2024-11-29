﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Models
{
    public class Product
    {
        
        public int MongoId { get; set; }
        public int Id { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }  
    }
}

