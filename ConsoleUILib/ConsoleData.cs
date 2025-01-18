using CadSimulation.Business;

namespace CadSimulation.ConsoleUI
{
  public class ConsoleData : IApplicationData
  {
    private readonly Dictionary<string, string> _listCommandLineParameters;

    public string GetValue(string key)
    {
      if (_listCommandLineParameters.TryGetValue(key, out var value))
      {
        return value;
      }
      else
      {
        return string.Empty;
      }
    }

    public ConsoleData(string[] args) 
    {
      _listCommandLineParameters = [];

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
            throw new ApplicationException($"Saving path \"{savingPath}\" doesn't exists.");
          }

          savingPathOk = !string.IsNullOrEmpty(Path.GetFileName(savingPath));
        }
        if (savingPathOk)
        {
          _listCommandLineParameters.Add("SavingPath", savingPath);
        }
        else
        {
          throw new ApplicationException("Define saving path using command line parameter. Example: CadSimulation.exe --path \"C:\\Projects\\ShapeData.txt\"");
        }
      }
      index = args.ToList().IndexOf("--json");
      _listCommandLineParameters.Add("JsonFormat", (index >= 0).ToString());


      index = args.ToList().IndexOf("--url");
      if (index >= 0)
      {
        // "url" expected right after

        Uri? urlPath = null;
        bool urlOk = (index + 1 < args.Length) && Uri.TryCreate(args[index + 1], UriKind.Absolute, out urlPath);
        if (urlOk)
        {
          _listCommandLineParameters.Add("UrlPath", urlPath!.AbsoluteUri);
        }
        else
        {
          throw new ApplicationException("Define \"url\" using command line parameter. Example: CadSimulation.exe --url \"http://127.0.0.1:8282/shapes\"");
        }
      }
    }
  }
}
