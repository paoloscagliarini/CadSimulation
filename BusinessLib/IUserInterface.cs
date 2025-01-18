namespace CadSimulation.Business
{
  /// <summary>
  /// Manages user interactions with the application
  /// </summary>
  public interface IUserInterface
  {
    /// <summary>
    /// Returns "true" if user has decided to quit application
    /// </summary>
    bool Quit { get; }

    /// <summary>
    /// Show the menu with all items
    /// </summary>
    void DisplayMenu();

    /// <summary>
    /// Returns the menu item choosen by the user
    /// </summary>
    /// <returns></returns>
    MenuItem? GetUserChoice();

    /// <summary>
    /// Returns the text entered by the user. Typically when user digit a phrase and the press "enter"
    /// </summary>
    /// <returns></returns>
    string? UserInput();

    /// <summary>
    /// Show a text on the screen
    /// </summary>
    /// <param name="text">Text to show</param>
    void MessageText(string text);
  }
}
