using CadSimulation.DomainClasses;

namespace CadSimulation
{
  internal class FileSystemAdapter : IFileAdapter
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
      if (_json)
      {
        using (StreamWriter sw = new StreamWriter(_filePath))
        {
          sw.WriteLine(JsonSerializerWrapper.Serialize(listOfShapes));
        }
      }
      else
      {
        using (StreamWriter sw = new StreamWriter(_filePath))
        {
          sw.Write(CustomFormatSerializer.Serialize(listOfShapes));
        }
      }
    }
    
    public List<IShape> Load()
    {
      if (_json)
      {
        using (StreamReader sr = new StreamReader(_filePath))
        {
          return JsonSerializerWrapper.Deserialize(sr.ReadToEnd());
        }
      }
      else
      {
        using (StreamReader sr = new StreamReader(_filePath))
        {
          return CustomFormatSerializer.Deserialize(sr.ReadToEnd());
        }
      }
    }
  }
}
