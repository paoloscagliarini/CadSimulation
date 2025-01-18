// See https://aka.ms/new-console-template for more information
using CadSimulation;
using CadSimulation.Business;
using CadSimulation.ConsoleUI;

/*
 Examples of running console application:
  1) save data to file system in custom format
    --path "c:\Projects\CadSimulation.txt"
  2) save data to file system in json format
    --path "c:\Projects\CadSimulation.json" --json
  3) save data to web server in custom format
    --url "http://127.0.0.1:8282/" 
  4) save data to web server in json format
    --url "http://127.0.0.1:8282/" --json 
*/

List<IShape> shapes = new List<IShape>();
MenuItem? menuItem;
ManageData manageData;

ConsoleData consoleData = new ConsoleData(args);
IUserInterface consoleUI = new ConsoleUI();

while (!consoleUI.Quit)
{
  consoleUI.DisplayMenu();
  menuItem = consoleUI.GetUserChoice();
  if (menuItem == null)
  {
    consoleUI.MessageText("");
    consoleUI.MessageText("Key not recognized");
    continue;
  }

  switch (menuItem.Code)
  {
    case "l":
      consoleUI.MessageText("");
      consoleUI.MessageText($"List of shapes (count={shapes.Count})");
      if (shapes.Count == 0)
      {
        consoleUI.MessageText("list is empty");
      }
      else
      {
        shapes.ForEach(s =>
        {
          consoleUI.MessageText(s.Descr());
        });
      }
      break;

    case "s":
      consoleUI.MessageText("Square. Value for side:\t");
      var side = Int32.Parse(consoleUI.UserInput()!);
      shapes.Add(new Square(side));
      break;

    case "r":
      consoleUI.MessageText("Rectangle.\nValue for height:\t");
      var hight = Int32.Parse(consoleUI.UserInput()!);
      consoleUI.MessageText("value for width:\t");
      var weidth = Int32.Parse(consoleUI.UserInput()!);
      shapes.Add(new Rectangle(hight, weidth));
      break;

    case "t":
      consoleUI.MessageText("Triangle.\nValue for height:\t");
      var height = Int32.Parse(consoleUI.UserInput()!);
      consoleUI.MessageText("value for base:\t");
      var width = Int32.Parse(consoleUI.UserInput()!);
      shapes.Add(new Triangle(height, width));
      break;

    case "c":
      consoleUI.MessageText("Circle. Value for radius:\t");
      var radius = Int32.Parse(consoleUI.UserInput()!);
      shapes.Add(new Circle(radius));
      break;

    case "a":
      double area = 0;
      foreach (var s in shapes)
        area += s.Area();

      consoleUI.MessageText($"Total area:{area}");
      break;

    case "k": // save data
      manageData = new ManageData(consoleData);
      try
      {
        manageData.Save(shapes);
      }
      catch (Exception ex)
      {
        consoleUI.MessageText("");
        consoleUI.MessageText(ex.Message);
        return;
      }
      consoleUI.MessageText("");
      consoleUI.MessageText($"-- Saved {shapes.Count} shape(s) --");
      break;

    case "w": // fetch data
      manageData = new ManageData(consoleData);
      try
      {
        shapes = manageData.Load();
      }
      catch (Exception ex)
      {
        consoleUI.MessageText("");
        consoleUI.MessageText(ex.Message);
      }
      consoleUI.MessageText("");
      consoleUI.MessageText($"-- Load {shapes.Count} shape(s) --");
      break;
  }
}
