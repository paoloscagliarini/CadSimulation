using CadSimulation.Business;
using CadSimulation.FileSystem;
using CadSimulation.Network;

namespace CadSimulation
{
  /// <summary>
  /// Utility for saving and loading list of shapes
  /// </summary>
  internal class ManageDataUtility
  {
    private readonly IDataAdapter _fileAdapter;

    public ManageDataUtility(IApplicationData applicationData)
    {
      // actionType = 1 -> manage data on file system, data in json format
      // actionType = 2 -> manage data on file system, data in custom format
      // actionType = 3 -> manage data with web server, data in json format
      // actionType = 4 -> manage data with web server, data in custom format

      int actionType = 0;
      string savingPath = applicationData.GetValue("SavingPath");
      bool jsonFormat = bool.Parse(applicationData.GetValue("JsonFormat"));

      if (!string.IsNullOrEmpty(savingPath))
      {
        actionType = jsonFormat ? 1 : 2;
      }
      else if (!string.IsNullOrEmpty(applicationData.GetValue("UrlPath")))
      {
        actionType = jsonFormat ? 3 : 4;
      }
      switch (actionType)
      {
        case 1: // manage data on file system, data in json format
        case 2: // manage data on file system, data in custom format
          _fileAdapter = new FileSystemAdapter(savingPath, jsonFormat);
          break;

        case 3: // manage data with web server, data in json format
        case 4: // manage data with web server, data in custom format
          _fileAdapter = new WebServiceAdapter(new Uri(applicationData.GetValue("UrlPath")), jsonFormat);
          break;

        default:
          throw new ApplicationException("Action non recognized");
      }
    }

    public void Save(List<IShape> listOfShapes)
    {
      _fileAdapter.Save(listOfShapes);
    }

    public List<IShape> Load()
    {
      return _fileAdapter.Load();
    }
  }
}
