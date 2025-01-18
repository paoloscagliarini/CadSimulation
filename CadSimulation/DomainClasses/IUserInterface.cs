namespace CadSimulation.DomainClasses
{
  internal interface IUserInterface
  {
    bool Quit { get; }

    void DisplayMenu();

    MenuItem? GetUserChoice();

    string? UserInput();

    void MessageText(string text);
  }
}
