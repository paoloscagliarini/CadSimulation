namespace CadSimulation.Business
{
  /// <summary>
  /// Save the information belonging to an application. For example the arguments of the command line in a console application
  /// </summary>
  public interface IApplicationData
  {
    string GetValue(string key);
  }
}
