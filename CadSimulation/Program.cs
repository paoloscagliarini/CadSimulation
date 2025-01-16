// See https://aka.ms/new-console-template for more information
using CadSimulation;
using System.Text.Json;
using System.Text.Json.Serialization;

List<Shape> shapes = new List<Shape>();
string savingPath = string.Empty;

int index = args.ToList().IndexOf("--path");
if (index >= 0)
{
  // "file name" expected right after

  bool savingPathOk = index + 1 < args.Length;
  if (savingPathOk)
  {
    savingPath = args[index + 1];
    if (!Directory.Exists(Path.GetDirectoryName(savingPath)))
    {
      Console.WriteLine("Saving path doesn't exists.");
      return;
    }

    savingPathOk = !string.IsNullOrEmpty(Path.GetFileName(savingPath));
  }
  if (!savingPathOk)
  {
    Console.WriteLine("Define saving path using command line parameter. Example: CadSimulation.exe --path C:\\Projects\\ShapeData.txt");
    return;
  }
}
index = args.ToList().IndexOf("--json");
bool jsonFormat = index >= 0;

while (true)
{
  Console.WriteLine(
"\nOptions:\n" +
"   's': insert a square\n" +
"   't': insert a triangle\n" +
"   'c': insert a circle\n" +
"   'r': insert a rectangle\n" +
"   'l': list all inserted shapes\n" +
"   'a': all shapres total area\n" +
"----\n" +
"   'k': save data\n" +
"   'w': fetch data\n" +
"   'q': quit");

  var k = Console.ReadKey(true);
  if (k.KeyChar == 'q')
    break;

  Shape? shape = null;
  switch (k.KeyChar)
  {
    case 'l':
      {
        shapes.ForEach(s =>
        {
          s.descr();
        });
      }
      continue;
    case 's':
      {
        Console.WriteLine("Square. Value for side:\t");
        var side = Int32.Parse(Console.ReadLine()!);
        shape = new Square(side); // Console.WriteLine("Square");
      }
      break;
    case 'r':
      {
        Console.WriteLine("Rectangle.\nValue for height:\t");
        var hight = Int32.Parse(Console.ReadLine()!);
        Console.WriteLine("value for width:\t");
        var weidth = Int32.Parse(Console.ReadLine()!);
        shape = new Rectangle(hight, weidth); // Console.WriteLine("Rectangle");
      }
      break;
    case 't':
      {
        Console.WriteLine("Triangle.\nValue for height:\t");
        var hight = Int32.Parse(Console.ReadLine()!);
        Console.WriteLine("value for base:\t");
        var weidth = Int32.Parse(Console.ReadLine()!);
        shape = new Triangle(hight, weidth); // Console.WriteLine("Triangle");
      }
      break;
    case 'c':
      Console.WriteLine("Circle. Value for radius:\t");
      var radius = Int32.Parse(Console.ReadLine()!);
      shape = new Circle(radius); // Console.WriteLine("Circle");
      break;
    case 'a':
      {
        double area = 0;
        foreach (var s in shapes)
          area += s.area();

        Console.WriteLine("Total area: {0}", area);
      }
      continue;

    case 'k': // save data
      if (shapes.Count == 0)
      {
        Console.WriteLine("Nothing to save");
        continue;
      }
      if (string.IsNullOrEmpty(savingPath))
      {
        Console.WriteLine("File path non defined. Add \"--path\" to command line. Example: CadSimulation.exe --path C:\\Projects\\ShapeData.txt");
        continue;
      }

      using (StreamWriter sw = new StreamWriter(savingPath))
      {
        if (jsonFormat)
        {
          List<object> listOfShapes = new List<object>();
          foreach (var theShape in shapes)
          {
            listOfShapes.Add(theShape);
          }
          sw.WriteLine(JsonSerializer.Serialize(listOfShapes));
        }
        else
        {
          foreach (Shape theShape in shapes)
          {
            sw.WriteLine(theShape.saveAsText());
          }
        }
      }
      Console.WriteLine("Data saved");
      continue;

    case 'w': // fetch data
      if (string.IsNullOrEmpty(savingPath))
      {
        Console.WriteLine("File path non defined. Add \"--path\" to command line. Example: CadSimulation.exe --path C:\\Projects\\ShapeData.txt");
        continue;
      }

      shapes.Clear();

      using (StreamReader sr = new StreamReader(savingPath))
      {
        if (jsonFormat)
        {
          List<object> listOfShapes = JsonSerializer.Deserialize<List<object>>(sr.ReadToEnd())!;
          foreach (var theShape in listOfShapes)
          {
            Shape trySquare = new Square((JsonElement)theShape);
            if (trySquare.IsValid)
              shapes.Add(trySquare);
            else
            {
              Shape tryRectangle = new Rectangle((JsonElement)theShape);
              if (tryRectangle.IsValid)
                shapes.Add(tryRectangle);
              else
              {
                Shape tryCircle = new Circle((JsonElement)theShape);
                if (tryCircle.IsValid)
                  shapes.Add(tryCircle);
                else
                {
                  Shape tryTriangle = new Triangle((JsonElement)theShape);
                  if (tryTriangle.IsValid)
                    shapes.Add(tryTriangle);
                }
              }
            }
          }
        }
        else
        {
          while (!sr.EndOfStream)
          {
            string? rowData = sr.ReadLine();

            Shape trySquare = new Square(rowData!);
            if (trySquare.IsValid)
              shapes.Add(trySquare);
            else
            {
              Shape tryRectangle = new Rectangle(rowData!);
              if (tryRectangle.IsValid)
                shapes.Add(tryRectangle);
              else
              {
                Shape tryCircle = new Circle(rowData!);
                if (tryCircle.IsValid)
                  shapes.Add(tryCircle);
                else
                {
                  Shape tryTriangle = new Triangle(rowData!);
                  if (tryTriangle.IsValid)
                    shapes.Add(tryTriangle);
                }
              }
            }
          }
        }
      }
      Console.WriteLine($"Data loaded. Found {shapes.Count} shapes.");
      continue;
  }
  shapes.Add(shape!);
}

namespace CadSimulation
{
  internal interface Shape
  {
    void descr();
    double area();

    /// <summary>
    /// Get the shape's type. Example: "Square"
    /// </summary>
    string ShapeType { get; }

    /// <summary>
    /// Save current data as text mode
    /// </summary>
    /// <returns></returns>
    string saveAsText();

    string saveAsJson();
    
    /// <summary>
    /// Return "true" is current shape data is valid
    /// </summary>
    bool IsValid { get; }
  }
  internal class Square : Shape
  {
    [JsonPropertyName("Type")]
    public string ShapeType { get { return "Square"; } }
    public int Side { get; private set; }

    private readonly bool _isValid;
    bool Shape.IsValid { get { return _isValid; } }

    public Square(int side)
    {
      Side = side;
      _isValid = true;
    }

    /// <summary>
    /// Deserialize data and save information to shape properties
    /// </summary>
    /// <param name="serializedData">Serialized data from which to extract data</param>
    public Square(string serializedData)
    {
      _isValid = false;
      string[] values = serializedData.Split(" ", StringSplitOptions.RemoveEmptyEntries);
      if (values.Length == 2)
      {
        if (values[0] == "S")
        {
          if (int.TryParse(values[1], out int side))
          {
            Side = side;
            _isValid = true;
          }
        }
      }
    }

    /// <summary>
    /// Deserialize data and save information to shape properties
    /// </summary>
    /// <param name="objectData">Shape data in json format</param>
    public Square(JsonElement objectData)
    {
      _isValid = objectData.GetProperty("Type").ToString() == ShapeType;
      if (_isValid)
      {
        Side = int.Parse(objectData.GetProperty("Side").ToString());
      }
    }

    double Shape.area()
    {
      return Side * Side;
    }

    void Shape.descr()
    {
      Console.WriteLine($"Square, side: {Side}");
    }
    
    string Shape.saveAsText()
    {
      return $"S {Side}";
    }

    string Shape.saveAsJson()
    {
      return JsonSerializer.Serialize(this);
    }
  }
  
  internal class Rectangle : Shape
  {
    [JsonPropertyName("Type")]
    public string ShapeType { get { return "Rectangle"; } }

    public int Height {  get; private set; }
    public int Width { get; private set; }
    
    private readonly bool _isValid;
    bool Shape.IsValid { get { return _isValid; } }

    public Rectangle(int height, int width)
    {
      Height = height;
      Width = width;
      _isValid = true;
    }

    /// <summary>
    /// Deserialize data and save information to shape properties
    /// </summary>
    /// <param name="serializedData">Serialized data from which to extract data</param>
    public Rectangle(string serializedData)
    {
      _isValid = false;
      string[] values = serializedData.Split(" ", StringSplitOptions.RemoveEmptyEntries);

      if (values.Length == 3)
      {
        if (values[0] == "R")
        {
          if (int.TryParse(values[1], out int height))
          {
            Height = height;
            if (int.TryParse(values[2], out int width))
            {
              Width = width;
              _isValid = true;
            }
          }
        }
      }
    }

    /// <summary>
    /// Deserialize data and save information to shape properties
    /// </summary>
    /// <param name="objectData">Shape data in json format</param>
    public Rectangle(JsonElement objectData)
    {
      _isValid = objectData.GetProperty("Type").ToString() == ShapeType;
      if (_isValid)
      {
        Height = int.Parse(objectData.GetProperty("Height").ToString());
        Width = int.Parse(objectData.GetProperty("Width").ToString());
      }
    }

    double Shape.area()
    {
      return Height * Width;
    }

    void Shape.descr()
    {
      Console.WriteLine($"Rectangle, height: {Height}, width: {Width}");
    }
    string Shape.saveAsText()
    {
      return $"R {Height} {Width}";
    }

    string Shape.saveAsJson()
    {
      return JsonSerializer.Serialize(this);
    }
  }

  internal class Circle : Shape
  {
    [JsonPropertyName("Type")]
    public string ShapeType { get { return "Circle"; } }

    public int Radius { get; private set; }
    readonly bool _isValid;
    bool Shape.IsValid { get { return _isValid; } }

    public Circle(int radius)
    {
      Radius = radius;
      _isValid = true;
    }

    /// <summary>
    /// Deserialize data and save information to shape properties
    /// </summary>
    /// <param name="serializedData">Serialized data from which to extract data</param>
    public Circle(string serializedData)
    {
      _isValid = false;
      string[] values = serializedData.Split(" ", StringSplitOptions.RemoveEmptyEntries);
      if (values.Length == 2)
      {
        if (values[0] == "C")
        {
          if (int.TryParse(values[1], out int radius))
          {
            Radius = radius;
            _isValid = true;
          }
        }
      }
    }

    /// <summary>
    /// Deserialize data and save information to shape properties
    /// </summary>
    /// <param name="objectData">Shape data in json format</param>
    public Circle(JsonElement objectData)
    {
      _isValid = objectData.GetProperty("Type").ToString() == ShapeType;
      if (_isValid)
      {
        Radius = int.Parse(objectData.GetProperty("Radius").ToString());
      }
    }

    double Shape.area()
    {
      return Radius * Radius * 3.1416;
    }

    void Shape.descr()
    {
      Console.WriteLine($"Circle, radius: {Radius}");
    }
    string Shape.saveAsText()
    {
      return $"C {Radius}";
    }

    string Shape.saveAsJson()
    {
      return JsonSerializer.Serialize(this);
    }
  }

  internal class Triangle : Shape
  {
    [JsonPropertyName("Type")]
    public string ShapeType { get { return "Triangle"; } }

    public int Base { get; private set; }
    public int Height {  get; private set; }
    readonly bool _isValid;
    bool Shape.IsValid { get { return _isValid; } }

    public Triangle(int b, int h)
    {
      Base = b;
      Height = h;
      _isValid = true;
    }

    /// <summary>
    /// Deserialize data and save information to shape properties
    /// </summary>
    /// <param name="serializedData">Serialized data from which to extract data</param>
    public Triangle(string serializedData)
    {
      _isValid = false;
      string[] values = serializedData.Split(" ", StringSplitOptions.RemoveEmptyEntries);

      if (values.Length == 3)
      {
        if (values[0] == "T")
        {
          if (int.TryParse(values[1], out int ibase))
          {
            Base = ibase;
            if (int.TryParse(values[2], out int height))
            {
              Height = height;
              _isValid = true;
            }
          }
        }
      }
    }

    /// <summary>
    /// Deserialize data and save information to shape properties
    /// </summary>
    /// <param name="objectData">Shape data in json format</param>
    public Triangle(JsonElement objectData)
    {
      _isValid = objectData.GetProperty("Type").ToString() == ShapeType;
      if (_isValid)
      {
        Base = int.Parse(objectData.GetProperty("Base").ToString());
        Height = int.Parse(objectData.GetProperty("Height").ToString());
      }
    }

    double Shape.area()
    {
      return Base * Height / 2;
    }
    void Shape.descr()
    {
      Console.WriteLine($"Triangle, base: {Base}, height: {Height}");
    }
    string Shape.saveAsText()
    {
      return $"T {Base} {Height}";
    }

    string Shape.saveAsJson()
    {
      return JsonSerializer.Serialize(this);
    }
  }
}

