namespace CadSimulation.Business
{
  public interface IShape
  {
    /// <summary>
    /// Returns the shape's code. Examples: "S", "T"
    /// </summary>
    string ShapeCode { get; }

    /// <summary>
    /// Returns the shape's type. Examples: "Square", "Triangle"
    /// </summary>
    string ShapeType { get; }

    string Descr();
    
    double Area();
  }
}
