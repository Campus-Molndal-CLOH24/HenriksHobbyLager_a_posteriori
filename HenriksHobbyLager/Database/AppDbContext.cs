using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options) // Initialiserar databaskontexten med Entity Framework Core och dess inställningar
    {
        // Representerar tabellen "Products" i databasen
        public DbSet<Product> Products { get; set; }
    }
}