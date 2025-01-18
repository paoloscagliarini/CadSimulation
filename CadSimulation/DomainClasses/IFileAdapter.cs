namespace CadSimulation.DomainClasses
{
  internal interface IFileAdapter
  {
    void Save(List<IShape> listOfShapes);

    List<IShape> Load();
  }
}
