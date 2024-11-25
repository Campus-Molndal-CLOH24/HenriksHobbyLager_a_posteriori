using RefactoringExercise.Database; // För datalagerhantering
using RefactoringExercise.Models; // För affärslogik och UI-modellen

namespace RefactoringExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            // Här skapas en instans av ProductRepository som hanterar all logik för att spara, hämta och uppdatera produkter i databasen.
            // Genom att använda ett repository får man en tydlig separation mellan datalagringslogik och resten av applikationen.
            var repository = new ProductRepository();

            // Här skapas ProductFacade som fungerar som ett mellanlager mellan användargränssnittet och datalagret.
            // Hä'r injectas repository i facede, så att den kan använda repository för att utföra databasoperationer.
            // Det gör att ConsoleUi bara behöver prata med facade och slipper bry sig om hur repository fungerar.
            var facade = new ProductFacade(repository);

            // Nu skapas ConsoleUi, där all interaktion med användaren sker.
            // Injectar facaden här så att användargränssnittet enkelt kan använda affärslogiken via facaden utan att känna till repository.
            var ui = new ConsoleUi(facade);

            // Här startar jag applikationen genom att köra huvudmetoden i ConsoleUi.
            // Det är den som hanterar allt användarflöde via konsolen och låter användaren utföra olika operationer.
            ui.Run();
        }
    }
}