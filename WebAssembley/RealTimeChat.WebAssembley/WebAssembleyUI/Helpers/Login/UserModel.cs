using System.Runtime.InteropServices.ComTypes;
using HelperLibrary.Models.Http;

namespace WebAssembleyUI.Helpers.Login;

public class UserModel:IDataModel
{
    public string Username;
    public string Password;
    public IDictionary<string, string> GetData()
    {
        var dictionary = new Dictionary<string, string>()
        {
            { "UserName", Username },
            { "Password", Password },
        };
        return dictionary;
    }
}