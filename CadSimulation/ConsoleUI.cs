using CadSimulation.DomainClasses;

namespace CadSimulation
{
  internal class ConsoleUI : IUserInterface
  {
    private readonly MenuData _menuData;

    public bool Quit { get; private set; }

    public ConsoleUI()
    {
      _menuData = new MenuData();
      Quit = false;
    }

    public void DisplayMenu()
    {
      MessageText("-----------");
      MessageText("Options:");
      foreach (MenuItem menuItem in _menuData.Items)
      {
        MessageText($"\t'{menuItem.Code}': {menuItem.Description}");
      }
    }

    public MenuItem? GetUserChoice()
    {
      var k = Console.ReadKey(true);
      MenuItem? menuItem = _menuData.Items.FirstOrDefault(x => x.Code == k.KeyChar.ToString());
      Quit = (menuItem != null) && (menuItem!.Code == "q");
      return menuItem;
    }

    public string? UserInput()
    {
      return Console.ReadLine();
    }


    public void MessageText(string text)
    {
      Console.WriteLine(text);
    }

  }
}
