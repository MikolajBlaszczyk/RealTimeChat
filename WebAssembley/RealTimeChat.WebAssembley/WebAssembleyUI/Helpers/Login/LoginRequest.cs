
using System.Text;
using HelperLibrary.Helpers.Http;
using HelperLibrary.Models.Http;
using Newtonsoft.Json;

namespace WebAssembleyUI.Helpers.Login
{
    public class LoginHelper:RequestHandler
    {
        public bool Validate(IDictionary<string, string> data) =>
            data["Password"] is not null && data["UserName"] is not null;

        public override async Task<bool> HandleRequest(HttpClient client, IDataModel model,string url)
        {
            var data = model.GetData();
            
            if (Validate(data) == false) 
                return false;

            var response = await SendRequest(client,data,url);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}
