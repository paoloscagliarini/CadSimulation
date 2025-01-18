using System.Text.Json;
using CadSimulation.Business;

namespace CadSimulation.Network
{
  /// <summary>
  /// Manages comunications using web services
  /// </summary>
  public class WebServiceAdapter : IDataAdapter
  {
    private readonly Uri _url;
    private readonly bool _json;

    public WebServiceAdapter(Uri uri, bool json)
    {
      _url = uri;
      _json = json;
    }

    public void Save(List<IShape> listOfShapes)
    {
      var task = Task.Run(async () => await PostData(listOfShapes));
      task.Wait();
      if (task.Result != System.Net.HttpStatusCode.OK)
        throw new ApplicationException("Error saving data using web service");
    }

    public List<IShape> Load()
    {
      var task = Task.Run(GetData);
      task.Wait();
      return task.Result;
    }

    private async Task<System.Net.HttpStatusCode> PostData(List<IShape> listOfShapes)
    {
      StringContent postData;

      if (_json)
      {
        List<object> listOfObjectShapes = listOfShapes.ToList<object>();
        postData = new StringContent(JsonSerializer.Serialize(listOfObjectShapes));
      }
      else
      {
        postData = new StringContent(CustomFormatSerializer.Serialize(listOfShapes));
      }

      HttpClient client = new HttpClient();
      try
      {
        HttpResponseMessage response = await client.PostAsync($"{_url}", postData);
        return response.StatusCode;
      }
      catch (Exception ex)
      {
        throw new ApplicationException("Errore sending data to web server. " + ex.Message);
      }
    }

    private async Task<List<IShape>> GetData()
    {
      string savedData;
      HttpClient client = new HttpClient();

      try
      {
        HttpResponseMessage response = await client.GetAsync($"{_url}");
        savedData = await response.Content.ReadAsStringAsync();
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
          throw new ApplicationException("Error fetching data from web service");
        }
      }
      catch (Exception ex)
      {
        throw new ApplicationException("Error fetching data from web service. " + ex.Message);
      }

      if (string.IsNullOrEmpty(savedData))
        return new List<IShape>();

      if (_json)
        return JsonSerializerWrapper.Deserialize(savedData);
      else
        return CustomFormatSerializer.Deserialize(savedData);
    }
  }
}
