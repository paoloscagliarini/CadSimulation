namespace CadSimulation.Business
{
  public interface IShape
  {
    string ShapeCode { get; }

    /// <summary>
    /// Get the shape's type. Example: "Square"
    /// </summary>
    string ShapeType { get; }

    string Descr();
    
    double Area();
  }
}
