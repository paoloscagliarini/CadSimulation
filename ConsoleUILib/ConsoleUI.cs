using CadSimulation.Business;

namespace CadSimulation.ConsoleUI
{
  /// <summary>
  /// Manages user interactions with the console application
  /// </summary>
  public class ConsoleUI : IUserInterface
  {
    private readonly ConsoleMenu _consoleMenu;

    public bool Quit { get; private set; }

    public ConsoleUI()
    {
      _consoleMenu = new ConsoleMenu();
      Quit = false;
    }

    public void DisplayMenu()
    {
      MessageText("-----------");
      MessageText("Options:");
      foreach (MenuItem menuItem in _consoleMenu.Items)
      {
        MessageText($"\t'{menuItem.Code}': {menuItem.Description}");
      }
    }

    public MenuItem? GetUserChoice()
    {
      var k = Console.ReadKey(true);
      MenuItem? menuItem = _consoleMenu.Items.FirstOrDefault(x => x.Code == k.KeyChar.ToString());
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
