using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserSafetyWeb.Entities;

namespace UserSafetyWeb.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;
        public string SuccessMessage { get; set; } = string.Empty;

        public RegisterModel ( IHttpClientFactory httpClientFactory )
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnPostAsync ()
        {
            var client = _httpClientFactory.CreateClient ("UserSafetyAPI");

            var payload = new { username = Username, password = Password };
            var response = await client.PostAsJsonAsync ("/api/Authentication/register", payload);

            if (response.IsSuccessStatusCode)
            {
                SuccessMessage = "Registrering lyckades! Du kan nu logga in.";
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<ErrorResponse> ();
                ErrorMessage = error?.Message ?? "Något gick fel.";
            }

            return Page ();
        }
    }
}