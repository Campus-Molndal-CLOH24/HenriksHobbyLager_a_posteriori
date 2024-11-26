using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager_a_posteriori.Models;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=products.db");
    }
}


