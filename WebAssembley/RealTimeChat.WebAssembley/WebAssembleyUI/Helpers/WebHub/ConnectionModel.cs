using HelperLibrary.Models.Http;

namespace WebAssembleyUI.Helpers.WebHub
{
    public class ConnectionModel:IDataModel
    {
        public string ConnectionId { get; set; }
        public IDictionary<string, string> GetData()
        {
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "ConnectionID", ConnectionId },
            };

            return data;
        }
    }
}
