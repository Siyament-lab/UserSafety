using System.ComponentModel.DataAnnotations;

namespace UserSafetyAPI.DTOs;

public class RegisterRequest
{
    [Required (ErrorMessage = "Användarnamn krävs.")]
    [StringLength (50, MinimumLength = 5, ErrorMessage = "Användarnamn måste vara mellan 5 och 50 tecken.")]
    public string Username { get; set; } = string.Empty;

    [Required (ErrorMessage = "Lösenord krävs.")]
    [StringLength (100, MinimumLength = 6, ErrorMessage = "Lösenord måste vara minst 6 tecken.")]
    public string Password { get; set; } = string.Empty;
}