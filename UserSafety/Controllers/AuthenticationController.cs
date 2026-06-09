
using Microsoft.AspNetCore.Mvc;
using UserSafetyAPI.Entities;
using UserSafetyAPI.DTOs;

namespace UserSafetyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        //Temporär in-memory datalagring för användare, ersätts med EF core i samband med SQL-databas skapandet.
        private static readonly Dictionary<string, User> _users = new();

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_users.ContainsKey(request.Username))
            {
                return BadRequest(new { message = "Användarnamn är redan taget." });
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);

            var user = new User
            {
                Id = _users.Count + 1,
                UserName = request.Username,
                PasswordHash = passwordHash // Hasha lösenordet
            };
            _users.Add(user.UserName, user);
            return Ok(new { message = "Registrering lyckades." });
        }
    }
}
