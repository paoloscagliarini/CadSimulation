// See https://aka.ms/new-console-template for more information
using CadSimulation;

List<Shape> shapes = new List<Shape>();

static string? GetPathFromCommandLine(string[] commandLine)
{
  string? savePath = null;
  if (commandLine.Length >= 2)
  {
    if (commandLine[0].Equals("--path", StringComparison.CurrentCultureIgnoreCase))
    {
      savePath = commandLine[1];
    }
  }
  if (string.IsNullOrEmpty(savePath))
  {
    Console.WriteLine("Define saving path using parameter \"--path [path where to save]\".");
  }
  if (!Directory.Exists(Path.GetDirectoryName(savePath)))
  {
    Console.WriteLine("Saving path doesn't exists.");
  }
  return savePath;
}

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

  string? savePath;
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
      savePath = GetPathFromCommandLine(args);
      if (!string.IsNullOrEmpty(savePath))
      {
        using (StreamWriter sw = new StreamWriter(savePath!))
        {
          foreach (Shape theShape in shapes)
          {
            sw.WriteLine(theShape.saveAsText());
          }
        }
        Console.WriteLine("Data saved as text.");
      }
      continue;

    case 'w': // fetch data
      savePath = GetPathFromCommandLine(args);
      if (!string.IsNullOrEmpty(savePath))
      {
        shapes.Clear();
        using (StreamReader sr = new StreamReader(savePath))
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

        Console.WriteLine($"Data loaded. Found {shapes.Count} shapes.");
        continue;
      }
      break;
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
    /// Save current data as text mode
    /// </summary>
    /// <returns></returns>
    string saveAsText();
    
    /// <summary>
    /// Return "true" is current shape data is valid
    /// </summary>
    bool IsValid { get; }
  }
  internal class Square : Shape
  {
    readonly int _side;
    readonly bool _isValid;
    bool Shape.IsValid { get { return _isValid; } }

    public Square(int side)
    {
      _side = side;
      _isValid = true;
    }

    /// <summary>
    /// Deserialize data and save information to shape properties. Extracts information and assigns it to properties
    /// </summary>
    /// <param name="serializeAsText">Serialized data from which to extract data</param>
    public Square(string serializeAsText)
    {
      _isValid = false;
      string[] values = serializeAsText.Split(" ", StringSplitOptions.RemoveEmptyEntries);
      if (values.Length == 2)
      {
        if (values[0] == "S")
        {
          if (int.TryParse(values[1], out _side))
          {
            _isValid = true;
          }
        }
      }
    }
    double Shape.area()
    {
      return _side * _side;
    }

    void Shape.descr()
    {
      Console.WriteLine($"Square, side: {_side}");
    }
    string Shape.saveAsText()
    {
      return $"S {_side}";
    }
  }
  internal class Rectangle : Shape
  {
    readonly private int _height;
    readonly private int _width;
    readonly bool _isValid;
    bool Shape.IsValid { get { return _isValid; } }

    public Rectangle(int height, int width)
    {
      _height = height;
      _width = width;
      _isValid = true;
    }

    /// <summary>
    /// Deserialize data and save information to shape properties. Extracts information and assigns it to properties
    /// </summary>
    /// <param name="serializeAsText">Serialized data from which to extract data</param>
    public Rectangle(string serializeAsText)
    {
      _isValid = false;
      string[] values = serializeAsText.Split(" ", StringSplitOptions.RemoveEmptyEntries);

      if (values.Length == 3)
      {
        if (values[0] == "R")
        {
          if (int.TryParse(values[1], out _height))
          {
            if (int.TryParse(values[2], out _width))
              _isValid = true;
          }
        }
      }
    }

    double Shape.area()
    {
      return _height * _width;
    }

    void Shape.descr()
    {
      Console.WriteLine($"Rectangle, height: {_height}, width: {_width}");
    }
    string Shape.saveAsText()
    {
      return $"R {_height} {_width}";
    }
  }

  internal class Circle : Shape
  {
    readonly int _radius;
    readonly bool _isValid;
    bool Shape.IsValid { get { return _isValid; } }

    public Circle(int radius)
    {
      _radius = radius;
      _isValid = true;
    }

    /// <summary>
    /// Deserialize data and save information to shape properties. Extracts information and assigns it to properties
    /// </summary>
    /// <param name="serializeAsText">Serialized data from which to extract data</param>
    public Circle(string serializeAsText)
    {
      _isValid = false;
      string[] values = serializeAsText.Split(" ", StringSplitOptions.RemoveEmptyEntries);
      if (values.Length == 2)
      {
        if (values[0] == "C")
        {
          if (int.TryParse(values[1], out _radius))
          {
            _isValid = true;
          }
        }
      }
    }

    double Shape.area()
    {
      return _radius * _radius * 3.1416;
    }

    void Shape.descr()
    {
      Console.WriteLine($"Circle, radius: {_radius}");
    }
    string Shape.saveAsText()
    {
      return $"C {_radius}";
    }
  }

  internal class Triangle : Shape
  {
    readonly int _base;
    readonly int _height;
    readonly bool _isValid;
    bool Shape.IsValid { get { return _isValid; } }

    public Triangle(int b, int h)
    {
      _base = b;
      _height = h;
      _isValid = true;
    }

    /// <summary>
    /// Deserialize data and save information to shape properties. Extracts information and assigns it to properties
    /// </summary>
    /// <param name="serializeAsText">Serialized data from which to extract data</param>
    public Triangle(string serializeAsText)
    {
      _isValid = false;
      string[] values = serializeAsText.Split(" ", StringSplitOptions.RemoveEmptyEntries);

      if (values.Length == 3)
      {
        if (values[0] == "T")
        {
          if (int.TryParse(values[1], out _base))
          {
            if (int.TryParse(values[2], out _height))
              _isValid = true;
          }
        }
      }
    }

    double Shape.area()
    {
      return _base * _height / 2;
    }
    void Shape.descr()
    {
      Console.WriteLine($"Triangle, base: {_base}, height: {_height}");
    }
    string Shape.saveAsText()
    {
      return $"T {_base} {_height}";
    }
  }
}

