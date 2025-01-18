using System.Text.Json.Serialization;

namespace CadSimulation.DomainClasses
{
  internal class Rectangle : IShape
  {
    [JsonIgnore]
    public string ShapeCode { get { return "R"; } }

    [JsonPropertyName("Type")]
    public string ShapeType { get { return "Rectangle"; } }

    public int Height { get; private set; }
    public int Width { get; private set; }

    public Rectangle(int height, int width)
    {
      Height = height;
      Width = width;
    }

    public double Area()
    {
      return Height * Width;
    }

    public string Descr()
    {
      return $"Rectangle, height: {Height}, width: {Width}";
    }
  }
}
