namespace Doubletten
{
    internal class Program
    {
        private static void Main(string[] args) {
            var consoleGui = new Gui(args);
            var controller = new Controller(consoleGui, new DoublettenSuche(
                new DirectoryCrawler(new HashBuilder(new FileLengthFinder())), new DoublettenFinder()));
            consoleGui.Run();
        }
    }
}