using HelperLibrary.Models.Http;

namespace HelperLibrary.Helpers.Http;

public interface IRequestHandler
{
    public Task<bool> HandleRequest(HttpClient client, IDataModel data, string url);
    public Task<HttpResponseMessage> SendRequest(HttpClient client, IDictionary<string, string> data, string url);
}