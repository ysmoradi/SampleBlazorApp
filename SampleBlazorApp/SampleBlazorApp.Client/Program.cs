using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using SampleBlazorApp.Client.Implementations;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SampleBlazorApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            var services = builder.Services;

            services.AddHttpClient(Microsoft.Extensions.Options.Options.DefaultName)
                .ConfigureHttpClient((serviceProvider, httpClient) =>
                {
                    httpClient.BaseAddress = new Uri(builder.Configuration["ApiAddress"]);
                })
                .AddHttpMessageHandler<AuthenticatedHttpMessageHandler>()
                .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
                .AddPolicyHandler(BuildHttpPollyPolicy);

            services.AddTransient<AuthenticatedHttpMessageHandler>();

            services.AddTransient(serviceProvider => serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient(Microsoft.Extensions.Options.Options.DefaultName));

            services.AddTelerikBlazor();

            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, SampleBlazorAppAuthenticationStateProvider>();
            services.AddTransient(serviceProvider => (SampleBlazorAppAuthenticationStateProvider)serviceProvider.GetRequiredService<AuthenticationStateProvider>());

            await builder.Build().RunAsync();
        }

        public static IAsyncPolicy<HttpResponseMessage> BuildHttpPollyPolicy(HttpRequestMessage requestMessage)
        {
            // https://github.com/App-vNext/Polly.Extensions.Http/blob/master/src/Polly.Extensions.Http/HttpPolicyExtensions.cs

            IAsyncPolicy<HttpResponseMessage> policy = Policy.Handle<HttpRequestException>() // HandleTransientHttpError
                .OrResult<HttpResponseMessage>((response) =>
                {
                    // if it's a known error, return false to prevent useless retry.
                    return (int)response.StatusCode >= 500 || response.StatusCode == HttpStatusCode.RequestTimeout; // TransientHttpStatusCodePredicate
                })
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                });

            return policy;
        }
    }
}
