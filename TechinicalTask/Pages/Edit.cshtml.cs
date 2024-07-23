using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnicalTaskAPI.Models;
using System.Text.Json;
using System.Text;

namespace TechinicalTask.Pages
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var httpClient = _httpClientFactory.CreateClient("TechnicalTaskAPI");

            using HttpResponseMessage response = await httpClient.GetAsync(id.ToString());

            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                Customer = await JsonSerializer.DeserializeAsync<Customer>(contentStream, options);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var jsonContent = new StringContent(JsonSerializer.Serialize(Customer),
                Encoding.UTF8,
                "application/json");

            var httpClient = _httpClientFactory.CreateClient("TechnicalTaskAPI");

            using HttpResponseMessage response = await httpClient.PutAsync(Customer.Id.ToString(), jsonContent);

            return RedirectToPage("./Index");
        }

        private bool CustomerExists(Guid id)
        {
            return Customer?.Id != null;
        }
    }
}
