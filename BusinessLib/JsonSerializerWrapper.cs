using System.Text.Json;

namespace CadSimulation.Business
{
  public static class JsonSerializerWrapper
  {
    public static string Serialize(List<IShape> shapes)
    {
      if (shapes == null)
        throw new ArgumentNullException(nameof(shapes));

      List<object> listOfObjectShapes = new List<object>();
      try
      {
          foreach (var theShape in shapes)
          {
            listOfObjectShapes.Add(theShape);
          }
        return JsonSerializer.Serialize(listOfObjectShapes);
      }
      catch (Exception ex)
      {
        throw new ApplicationException("Error saving list of shapes in json format. " + ex.Message);
      }
    }

    public static List<IShape> Deserialize(string serializedData)
    {
      JsonElement objectData;
      List<IShape> listOfShapes = new List<IShape>();

      List<object> listOfShapesFromJson = JsonSerializer.Deserialize<List<object>>(serializedData)!;
      foreach (var theShape in listOfShapesFromJson)
      {
        objectData = (JsonElement)theShape;
        if (objectData.GetProperty("Type").ToString() == "Square")
        {
          listOfShapes.Add(new Square(int.Parse(objectData.GetProperty("Side").ToString())));
        }
        else if (objectData.GetProperty("Type").ToString() == "Triangle")
        {
          listOfShapes.Add(new Triangle(int.Parse(objectData.GetProperty("Base").ToString()), int.Parse(objectData.GetProperty("Height").ToString())));
        }
        else if (objectData.GetProperty("Type").ToString() == "Circle")
        {
          listOfShapes.Add(new Circle(int.Parse(objectData.GetProperty("Radius").ToString())));
        }
        else if (objectData.GetProperty("Type").ToString() == "Rectangle")
        {
          listOfShapes.Add(new Rectangle(int.Parse(objectData.GetProperty("Height").ToString()), int.Parse(objectData.GetProperty("Width").ToString())));
        }
      }

      return listOfShapes;
    }
  }
}
