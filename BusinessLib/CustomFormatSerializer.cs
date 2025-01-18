using CadSimulation.Business;

namespace CadSimulation
{
  /// <summary>
  /// Performs serialization and deserialization of a list of shapes in "custom" format
  /// </summary>
  public static class CustomFormatSerializer
  {
    public static string Serialize(List<IShape> shapes)
    {
      if (shapes == null)
        throw new ArgumentNullException(nameof(shapes));

      string serializedData = string.Empty;

      foreach (var shape in shapes)
      {
        switch (shape.ShapeCode)
        {
          case "S":
            serializedData += $"{shape.ShapeCode} {((Square)shape).Side}\n";
            break;

          case "T":
            serializedData += $"{shape.ShapeCode} {((Triangle)shape).Base} {((Triangle)shape).Height}\n";
            break;

          case "C":
            serializedData += $"{shape.ShapeCode} {((Circle)shape).Radius}\n";
            break;

          case "R":
            serializedData += $"{shape.ShapeCode} {((Rectangle)shape).Height} {((Rectangle)shape).Width}\n";
            break;
        }
      }

      return serializedData;
    }

    public static List<IShape> Deserialize(string serializedData)
    {
      List<IShape> listOfShapes = new List<IShape>();
      int line = 0;

      List<string> rowsList = serializedData.Replace("\r", string.Empty).Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();

      foreach (string rowData in rowsList)
      {
        line++;

        string[] values = rowData.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (values.Length == 0)
        {
          throw new ApplicationException($"row n.{line} -> Invalid data");
        }
        switch (values[0])
        {
          case "S":
            if (values.Length == 2)
            {
              if (int.TryParse(values[1], out int side))
              {
                listOfShapes.Add(new Square(side));
                continue;
              }
            }
            throw new ApplicationException($"row n.{line} -> Square data not valid");

          case "T":
            if (values.Length == 3)
            {
              if (int.TryParse(values[1], out int thebase))
              {
                if (int.TryParse(values[2], out int height))
                {
                  listOfShapes.Add(new Triangle(thebase, height));
                  continue;
                }
              }
            }
            throw new ApplicationException($"row n.{line} -> Triangle data not valid");

          case "C":
            if (values.Length == 2)
            {
              if (int.TryParse(values[1], out int radius))
              {
                listOfShapes.Add(new Circle(radius));
                continue;
              }
            }
            throw new ApplicationException($"row n.{line} -> Circle data not valid");

          case "R":
            if (values.Length == 3)
            {
              if (int.TryParse(values[1], out int height))
              {
                if (int.TryParse(values[2], out int width))
                {
                  listOfShapes.Add(new Rectangle(height, width));
                  continue;
                }
              }
            }
            throw new ApplicationException($"row n.{line} -> Rectangle data not valid");

          default:
            throw new ApplicationException($"row n.{line} -> Shape type \"{values[0]}\" unmanaged");
        }
      }

      return listOfShapes;
    }
  }
}
