using Microsoft.AspNetCore.Components;
using SampleBlazorApp.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SampleBlazorApp.Client.Pages
{
    public partial class FetchData
    {
        [Inject]
        public HttpClient Http { get; set; }

        public WeatherForecast[] Forecasts { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
        }
    }
}
