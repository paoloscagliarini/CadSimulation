namespace CadSimulation.Business
{
  /// <summary>
  /// Adatper for comunications with external system
  /// </summary>
  public interface IDataAdapter
  {
    /// <summary>
    /// Persist a list of shapes in an external repository
    /// </summary>
    /// <param name="listOfShapes">List of shapes to save</param>
    void Save(List<IShape> listOfShapes);

    /// <summary>
    /// Load a list of shapes from external repository
    /// </summary>
    /// <returns></returns>
    List<IShape> Load();
  }
}
