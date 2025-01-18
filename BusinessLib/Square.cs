using System.Text.Json.Serialization;

namespace CadSimulation.Business
{
  public class Square : IShape
  {
    [JsonIgnore]
    public string ShapeCode { get { return "S"; } }

    [JsonPropertyName("Type")]
    public string ShapeType { get { return "Square"; } }

    public int Side { get; private set; }

    public Square(int side)
    {
      Side = side;
    }

    public double Area()
    {
      return Side * Side;
    }

    public string Descr()
    {
      return $"Square, side: {Side}";
    }
  }
}
