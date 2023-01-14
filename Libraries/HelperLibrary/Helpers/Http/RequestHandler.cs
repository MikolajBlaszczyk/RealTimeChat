using System.Text;
using System.Text.Json.Serialization;
using HelperLibrary.Models.Http;
using Newtonsoft.Json;
namespace HelperLibrary.Helpers.Http;

public class RequestHandler:IRequestHandler
{
    public virtual async Task<bool> HandleRequest(HttpClient client, IDataModel model,string url)
    {
        var data = model.GetData();
        var response = await SendRequest(client, data,url);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public virtual async Task<HttpResponseMessage> SendRequest(HttpClient client, IDictionary<string, string> data, string url)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, content);
        return response;
    }
}