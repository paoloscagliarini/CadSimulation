namespace CadSimulation.Business
{
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
