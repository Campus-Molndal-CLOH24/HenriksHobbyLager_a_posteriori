using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;



namespace HenriksHobbyLager_a_posteriori.Models { }
[Table("Toys")]
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; }
    public DateTime Created { get; set; } 
    public DateTime Updated { get; set; } 
}










