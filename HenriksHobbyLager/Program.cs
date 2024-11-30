using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using HenriksHobbyLager.Utilities;




namespace RefactoringExercise
{ 
    public class Program
    {
        static IDatabase _database;
        public static void Main(string[] args)
        {
            // Läser config.txt
            var (dbType, connectionString) = ConfigReader.ReadConfig("config.txt");

            //Skapar databasen som är vald
            IDatabase database = DatabaseFactory.CreateDatabase(dbType);

            //Ansluter till databasen
            database.Connect(connectionString);

            //Skapar en tabell om så behövs
            database.CreateTable();

            //Kör huvudmenyn

            var menu = new Menu(database);
            menu.MainMenu();

        }
    }
} 
