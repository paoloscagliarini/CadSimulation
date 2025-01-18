using System.Text.Json;
using CadSimulation.DomainClasses;

namespace CadSimulation
{
  internal class WebServiceAdapter : IFileAdapter
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
      StringContent postData;
      HttpClient client = new HttpClient();

      if (_json)
      {
        List<object> listOfObjectShapes = new List<object>();
        foreach (var theShape in listOfShapes)
        {
          listOfObjectShapes.Add(theShape);
        }

        string json = JsonSerializer.Serialize(listOfObjectShapes);
        postData = new StringContent(json);
        try
        {
          var task = Task.Run(async () => await client.PostAsync($"{_url}", postData));
          HttpResponseMessage response = task.Result;

          var task2 = Task.Run(async () => await response.Content.ReadAsStringAsync());
          if (response.StatusCode != System.Net.HttpStatusCode.OK)
          {
            throw new ApplicationException("Error saving data using web service");
          }
        }
        catch (Exception ex)
        {
          throw new ApplicationException("Errore sending data to web server. " + ex.Message);
        }
      }
      else
      {
        postData = new StringContent(CustomFormatSerializer.Serialize(listOfShapes));
        try
        {
          var task = Task.Run(async () => await client.PostAsync($"{_url}", postData));
          HttpResponseMessage response = task.Result;

          var task2 = Task.Run(async () => await response.Content.ReadAsStringAsync());
          if (response.StatusCode != System.Net.HttpStatusCode.OK)
          {
            throw new ApplicationException("Error saving data using web service");
          }
        }
        catch (Exception ex)
        {
          throw new ApplicationException("Error sending data to web server. " + ex.Message);
        }
      }
    }

    public List<IShape> Load()
    {
      string savedData;
      HttpClient client = new HttpClient();

      if (_json)
      {
        try
        {
          var task = Task.Run(async () => await client.GetAsync($"{_url}"));
          HttpResponseMessage response = task.Result;

          var task2 = Task.Run(async () => await response.Content.ReadAsStringAsync());
          savedData = task2.Result;

          if (response.StatusCode != System.Net.HttpStatusCode.OK)
          {
            throw new ApplicationException("Error fetching data from web service");
          }

          if (string.IsNullOrEmpty(savedData))
          {
            return new List<IShape>();
          }
        }
        catch (Exception ex)
        {
          throw new ApplicationException("Error fetching data from web service. " + ex.Message);
        }

        return JsonSerializerWrapper.Deserialize(savedData);
      }
      else
      {
        try
        {
          var task = Task.Run(async () => await client.GetAsync($"{_url}"));
          HttpResponseMessage response = task.Result;

          var task2 = Task.Run(async () => await response.Content.ReadAsStringAsync());
          savedData = task2.Result;

          if (response.StatusCode != System.Net.HttpStatusCode.OK)
          {
            throw new ApplicationException("Error fetching data from web service");
          }

          return CustomFormatSerializer.Deserialize(savedData);
        }
        catch (Exception ex)
        {
          throw new ApplicationException("Error fetching data from web service. " + ex.Message);
        }
      }
    }
  }
}
