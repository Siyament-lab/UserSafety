
using Microsoft.AspNetCore.Mvc;
using UserSafetyAPI.Entities;
using UserSafetyAPI.DTOs;
using UserSafetyAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace UserSafetyAPI.Controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;

        public AuthenticationController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //Metod för användar-registrering
        [HttpPost ("register")]
        public async Task<IActionResult> RegisterAsync ( [FromBody] RegisterRequest request )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest (ModelState);
            }
            if (await _dbcontext.Users.AnyAsync (u => u.UserName == request.Username))
            {
                return BadRequest (new { message = "Användarnamn är redan taget." });
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword (request.Password, workFactor: 12);

            var user = new User
            {
                UserName = request.Username,
                PasswordHash = passwordHash
            };
            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();
            return Ok (new { message = "Registrering lyckades." });
        }
        //Metod för användar-inloggning
        [HttpPost ("login")]
        public async Task<IActionResult> LoginAsync ( [FromBody] LoginRequest request )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest (ModelState);
            }
            var user = await _dbcontext.Users
                .FirstOrDefaultAsync(u => u.UserName == request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized (new { message = "Ogiltiga inloggningsuppgifter." });
            }
            // JWT-autentisering läggs till senare med egen branch
            return Ok (new { message = "Inloggning lyckades." });
        }
    }
}
