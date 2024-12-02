
// Denna anropar HenriksHobbyLager.ProgramManager.HenriksHobbyLager.ProgramManagement.HenriksHobbyLagerProgramManager.Run() som är en metod som innehåller allt som ska köras i programmet.
//För att hålla koden ren, skapade jag en egen klass för programlogiken. 
using HenriksHobbyLager.ProgramManagement;


internal class Program
{
    private static void Main(string[] args)
    {
        var manager = new HenriksHobbyLagerProgramManager();
        manager.Run();
    }
}
