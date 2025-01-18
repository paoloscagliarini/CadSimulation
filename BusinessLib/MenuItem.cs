namespace CadSimulation.Business
{
  /// <summary>
  /// Defines information about individual menu item, such as description that will be shown in the user iterface
  /// </summary>
  public class MenuItem
  {
    public readonly string Code;

    public readonly string Description;

    public MenuItem(string code, string description)
    {
      this.Code = code ?? throw new ArgumentNullException(nameof(code));
      this.Description = description ?? throw new ArgumentNullException(nameof(description));
    }
  }
}
