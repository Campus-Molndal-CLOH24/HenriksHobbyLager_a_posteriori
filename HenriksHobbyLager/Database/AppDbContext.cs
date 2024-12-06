using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options) // Entity Framework Core integration
    {
        public DbSet<Product> Products { get; set; }
    }
}