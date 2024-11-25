namespace RefactoringExercise.Models
{
    public class Product
    {
        // produktens unika identifierare
        public int Id { get; set; }

        // namn på produkt
        public string Name { get; set; }

        // pris för produkten
        public decimal Price { get; set; }

        // antalet produkter som finns i lager
        public int Stock { get; set; }

        // kategorin som produkten tillhör, t.ex. "Helikopter"
        public string Category { get; set; }

        // datum och tid då produkten skapades i systemet
        public DateTime Created { get; set; }

        // Datum och tid för senaste uppdatering av produktens information
        //  ? för att indikera att det kan vara tomt
        public DateTime? LastUpdated { get; set; }
    }
}