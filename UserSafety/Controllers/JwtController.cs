using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserSafetyAPI.Controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    [Authorize] //Ser till att giltig JWT skickas för att komma åt detta endpoint
    public class JwtController : ControllerBase
    {
        [HttpGet ("test")]
        public IActionResult Test ()
        {
            return Ok (new { message = "Du är autentiserad och har tillgång till detta endpoint!" });
        }
    }
}