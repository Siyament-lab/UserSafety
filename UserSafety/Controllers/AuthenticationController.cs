using Microsoft.AspNetCore.Mvc;
using UserSafetyAPI.Entities;
using UserSafetyAPI.DTOs;
using UserSafetyAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UserSafetyAPI.Controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;
        private readonly IConfiguration _configuration;

        public AuthenticationController(AppDbContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext;
            _configuration = configuration;
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
            // JWT-autentisering
            var token = GenerateJwtToken(user);
            return Ok (new { message = "Inloggning lyckades.", token });
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}