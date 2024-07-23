using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechnicalTaskAPI.Models;
using System.Text.Json;

namespace TechinicalTask.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Customer.Id = Guid.NewGuid();
            Customer.Created = DateTime.Now;

            var jsonContent = new StringContent(JsonSerializer.Serialize(Customer),
               Encoding.UTF8,
               "application/json");

            var httpClient = _httpClientFactory.CreateClient("TechnicalTaskAPI");

            using HttpResponseMessage response = await httpClient.PostAsync("", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Data was added successfully.";
                return RedirectToPage("./Index");
            }
            else
            {
                TempData["failure"] = "Operation was not successful";
                return RedirectToPage("./Index");
            }
        }
    }
}
