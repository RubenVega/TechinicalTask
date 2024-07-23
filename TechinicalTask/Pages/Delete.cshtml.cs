using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechnicalTaskAPI.Models;
using System.Text.Json;

namespace TechinicalTask.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task OnGetAsync(Guid? id)
        {
            var httpClient = _httpClientFactory.CreateClient("TechnicalTaskAPI");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            using HttpResponseMessage response = await httpClient.GetAsync(id.ToString());

            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                Customer = await JsonSerializer.DeserializeAsync<Customer>(contentStream, options);
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            var httpClient = _httpClientFactory.CreateClient("TechnicalTaskAPI");

            using HttpResponseMessage response = await httpClient.DeleteAsync(Customer.Id.ToString());

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Data was deleted successfully.";
                return RedirectToPage("Index");
            }
            else
            {
                TempData["failure"] = "Operation was not successful";
                return RedirectToPage("Index");
            }
        }
    }
}
