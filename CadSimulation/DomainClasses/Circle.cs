using System.Text.Json.Serialization;

namespace CadSimulation.DomainClasses
{
  internal class Circle : IShape
  {
    [JsonIgnore]
    public string ShapeCode { get { return "C"; } }

    [JsonPropertyName("Type")]
    public string ShapeType { get { return "Circle"; } }

    public int Radius { get; private set; }

    public Circle(int radius)
    {
      Radius = radius;
    }

    public double Area()
    {
      return Radius * Radius * 3.1416;
    }

    public string Descr()
    {
      return $"Circle, radius: {Radius}";
    }
  }
}
