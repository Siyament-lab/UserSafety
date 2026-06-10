using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserSafetyWeb.Entities;

namespace UserSafetyWeb.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;
        public string SuccessMessage { get; set; } = string.Empty;

        public LoginModel ( IHttpClientFactory httpClientFactory )
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnPostAsync ()
        {
            var client = _httpClientFactory.CreateClient ("UserSafetyAPI");

            var payload = new { username = Username, password = Password };
            var response = await client.PostAsJsonAsync ("/api/Authentication/login", payload);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse> ();
                SuccessMessage = $"Inloggning lyckades! Din token: {result?.Token}";
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<ErrorResponse> ();
                ErrorMessage = error?.Message ?? "Ogiltiga inloggningsuppgifter.";
            }

            return Page ();
        }
    }
}