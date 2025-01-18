using CadSimulation.Business;

namespace CadSimulation.ConsoleUI
{
  public class ConsoleMenu
  {
    public readonly List<MenuItem> Items;
    
    public ConsoleMenu()
    {
      Items =
      [
        new MenuItem("s", "insert a square"),
        new MenuItem("t", "insert a triangle"),
        new MenuItem("c", "insert a circle"),
        new MenuItem("r", "insert a rectangle"),
        new MenuItem("l", "list all inserted shapes"),
        new MenuItem("a", "all shapes total area"),
        new MenuItem("k", "save data"),
        new MenuItem("w", "fetch data"),
        new MenuItem("q", "quit")
      ];
    }
  }
}
