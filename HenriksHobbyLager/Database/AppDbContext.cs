using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HenriksNyaHobbyLager.db");
        }
        
    }
}