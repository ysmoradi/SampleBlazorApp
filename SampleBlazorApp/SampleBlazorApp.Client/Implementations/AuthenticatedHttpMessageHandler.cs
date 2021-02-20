using Microsoft.JSInterop;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SampleBlazorApp.Client.Implementations
{
    public class AuthenticatedHttpMessageHandler : DelegatingHandler
    {
        private readonly IJSRuntime _jSRuntime;

        public AuthenticatedHttpMessageHandler(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string access_token = await _jSRuntime.InvokeAsync<string>("getLocalStorageValue", "access_token");

            if (access_token != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // publish token expired event to go to login page
                // or
                // try to get new access token using refresh token
            }

            return response;
        }
    }
}
