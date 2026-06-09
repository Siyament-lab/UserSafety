using System.ComponentModel.DataAnnotations;

namespace UserSafetyAPI.DTOs;

public class LoginRequest
{
    [Required (ErrorMessage = "Användarnamn krävs.")]
    public string Username { get; set; } = string.Empty;

    [Required (ErrorMessage = "Lösenord krävs.")]
    public string Password { get; set; } = string.Empty;
}