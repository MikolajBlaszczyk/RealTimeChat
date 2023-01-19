using RealTimeChat.AccountLogic.Enums;

namespace RealTimeChat.AccountLogic.Models
{
    public class ResponseModel
    {
        public ResponseIdentityResult Result {get; set; }
        public string Message { get; set; }

        private ResponseModel(ResponseIdentityResult result, string message)
        {
            Result = result;
            Message = message;
        }

        public static ResponseModel CreateResponse(ResponseIdentityResult result, string message = "")
        {
            return new ResponseModel(result, message);
        }
    }
}
