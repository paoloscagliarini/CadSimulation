namespace CadSimulation.DomainClasses
{
  internal class MenuData
  {
    public readonly List<MenuItem> Items;
    
    public MenuData()
    {
      Items = new List<MenuItem>()
      {
        new MenuItem("s", "insert a square"),
        new MenuItem("t", "insert a triangle"),
        new MenuItem("c", "insert a circle"),
        new MenuItem("r", "insert a rectangle"),
        new MenuItem("l", "list all inserted shapes"),
        new MenuItem("a", "all shapes total area"),
        new MenuItem("k", "save data"),
        new MenuItem("w", "fetch data"),
        new MenuItem("q", "quit")
      };
    }
  }
}
