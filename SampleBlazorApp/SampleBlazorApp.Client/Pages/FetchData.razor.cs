using Microsoft.AspNetCore.Components;
using SampleBlazorApp.Shared;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace SampleBlazorApp.Client.Pages
{
    public partial class FetchData
    {
        [Inject]
        public HttpClient Http { get; set; }

        public WeatherForecast[] Forecasts { get; set; }

        public int TotalForecasts { get; set; }

        public async Task GetWeatherForecasts(GridReadEventArgs args)
        {
            Forecasts = (await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast"))
                .Skip(args.Request.Skip * args.Request.Page) // send page / skip / sort / filter etc to the server
                .Take(args.Request.PageSize)
                .ToArray();

            TotalForecasts = 50;
        }
    }
}
