using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager
{
    public static class Startup
    {
        public static void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data Source=HenriksNyaHobbyLager.db")
                .Options;

            var dbContext = new AppDbContext(options);
            dbContext.Database.EnsureCreated();

            var repository = new ProductRepository(dbContext);
            IProductFacade productFacade = new ProductFacade(repository);

            var consoleMenuHandler = new ConsoleMenuHandler(productFacade);
            consoleMenuHandler.ShowMainMenu();
        }
    }
}