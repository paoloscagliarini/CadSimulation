using System.Text.Json.Serialization;

namespace CadSimulation.DomainClasses
{
  internal class Triangle : IShape
  {
    [JsonIgnore]
    public string ShapeCode { get { return "T"; } }

    [JsonPropertyName("Type")]
    public string ShapeType { get { return "Triangle"; } }

    public int Base { get; private set; }
    public int Height { get; private set; }

    public Triangle(int b, int h)
    {
      Base = b;
      Height = h;
    }

    public double Area()
    {
      return Base * Height / 2;
    }
    public string Descr()
    {
      return $"Triangle, base: {Base}, height: {Height}";
    }
  }
}
