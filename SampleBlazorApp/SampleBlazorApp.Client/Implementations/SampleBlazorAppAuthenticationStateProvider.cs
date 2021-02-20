using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleBlazorApp.Client.Implementations
{
    public class SampleBlazorAppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;

        public SampleBlazorAppAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public void StateHasChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        AuthenticationState NoUser() => new AuthenticationState(user: new ClaimsPrincipal()); // due it has no ClaimsIdentity

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string access_token = await _jsRuntime.InvokeAsync<string>("getLocalStorageValue", "access_token");

            if (access_token == null)
                return NoUser();

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(Array.Empty<Claim>(), authenticationType: "Bearer")));
        }
    }
}
