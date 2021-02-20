using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SampleBlazorApp.Client.Implementations;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleBlazorApp.Client.Pages
{
    public partial class Login
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public SampleBlazorAppAuthenticationStateProvider SampleBlazorAppAuthenticationStateProvider { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public async Task PerformLogin()
        {
            if (Username == Password)
            {
                await JSRuntime.InvokeVoidAsync("setLocalStorageValue", "access_token", Guid.NewGuid().ToString());
                SampleBlazorAppAuthenticationStateProvider.StateHasChanged();
            }
        }
    }
}
