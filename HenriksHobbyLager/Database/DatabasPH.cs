using HenriksHobbyLager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Database
{
    public class DatabasPH
    {
        // Min fantastiska databas! Fungerar perfekt så länge datorn är igång
        private static readonly List<Product> _products = new();

        // Räknare för ID. Börjar på 1 för att 0 känns så negativt
        private static int _nextId = 1;
    }
}
