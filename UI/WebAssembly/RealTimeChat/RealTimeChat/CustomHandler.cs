namespace RealTimeChat
{
    public class CustomHandler : DelegatingHandler
    {
        private string setCookieHeader;

        public string CookieHeader
        {
            get
            {
                return setCookieHeader;
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.Headers.TryGetValues("Set-Cookie", out var setCookieValue))
            {
                setCookieHeader = setCookieValue.FirstOrDefault();
            }

            return response;
        }
    }
}
