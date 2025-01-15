// See https://aka.ms/new-console-template for more information
using CadSimulation;

List<Shape> shapes = new List<Shape>();

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
        Console.WriteLine("Rectangle.\nValue for hight:\t");
        var hight = Int32.Parse(Console.ReadLine()!);
        Console.WriteLine("value for weidth:\t");
        var weidth = Int32.Parse(Console.ReadLine()!);
        shape = new Rectangle(hight, weidth); // Console.WriteLine("Rectangle");
      }
      break;
    case 't':
      {
        Console.WriteLine("Triangle.\nValue for hight:\t");
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
  }
  shapes.Add(shape!);

}

namespace CadSimulation
{
  internal interface Shape
  {
    void descr();
    double area();
  }
  internal class Square : Shape
  {
    readonly int _side;
    public Square(int side)
    {
      _side = side;
    }
    double Shape.area()
    {
      return _side * _side;
    }

    void Shape.descr()
    {
      Console.WriteLine($"Square, side: {_side}");
    }
  }
  internal class Rectangle : Shape
  {
    readonly int _height;
    readonly int _width;
    public Rectangle(int height, int weidth)
    {
      _height = height;
      _width = weidth;
    }
    double Shape.area()
    {
      return _height * _width;
    }

    void Shape.descr()
    {
      Console.WriteLine($"Rectangle, height: {_height}, weidth: {_width}");
    }
  }
  internal class Circle : Shape
  {
    int _radius;
    public Circle(int radius)
    {
      _radius = radius;
    }

    double Shape.area()
    {
      return _radius * _radius * 3.1416;
    }

    void Shape.descr()
    {
      Console.WriteLine($"Circle, radius: {_radius}");
    }
  }
  internal class Triangle : Shape
  {
    int _base;
    int _height;
    public Triangle(int b, int h)
    {
      _base = b;
      _height = h;
    }
    double Shape.area()
    {
      return _base * _height / 2;
    }
    void Shape.descr()
    {
      Console.WriteLine($"Triangle, base: {_base}, height: {_height}");
    }
  }
}

