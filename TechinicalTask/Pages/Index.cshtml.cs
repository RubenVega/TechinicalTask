using Microsoft.AspNetCore.Mvc.RazorPages;
using TechnicalTaskAPI.Models;
using System.Text.Json;
using NuGet.Protocol;
using Microsoft.Extensions.Options;


namespace TechinicalTask.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IEnumerable<Customer> Customers { get;set; } = default!;

        public async Task OnGet()
        {
            var httpClient = _httpClientFactory.CreateClient("TechnicalTaskAPI");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            using HttpResponseMessage response = await httpClient.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                Customers = await JsonSerializer.DeserializeAsync<List<Customer>>(contentStream, options);
            }
        }
    }
}
