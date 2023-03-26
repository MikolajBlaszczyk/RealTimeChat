using System.Net;

namespace RealTimeChatClient
{
    //temporary Code
    internal static class  HttpHandler
    {
        private static CookieContainer Container;

        internal static void InitContainer(CookieContainer container)
        {
            Container = container;
        }

        internal static CookieContainer GetContainer()
        {
            return Container;
        } 
    }
}
