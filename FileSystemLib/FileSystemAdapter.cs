using CadSimulation.Business;

namespace CadSimulation.FileSystem
{
  /// <summary>
  /// Manages comunications with local file system
  /// </summary>
  public class FileSystemAdapter : IDataAdapter
  {
    private readonly string _filePath;
    private readonly bool _json;

    public FileSystemAdapter(string filePath, bool json)
    {
      _filePath = filePath;
      _json = json;
    }

    public void Save(List<IShape> listOfShapes)
    {
      using (StreamWriter sw = new StreamWriter(_filePath))
      {
        if (_json)
          sw.WriteLine(JsonSerializerWrapper.Serialize(listOfShapes));
        else
          sw.Write(CustomFormatSerializer.Serialize(listOfShapes));
      }
    }
    
    public List<IShape> Load()
    {
      using (StreamReader sr = new StreamReader(_filePath))
      {
        if (_json)
          return JsonSerializerWrapper.Deserialize(sr.ReadToEnd());
        else
          return CustomFormatSerializer.Deserialize(sr.ReadToEnd());
      }
    }
  }
}
