using Microsoft.AspNetCore.Mvc;
using LetGoNowApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LetGoNowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LetGoNowDbContext _context;

        public AuthController(LetGoNowDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.PasswordHash == loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(new { message = "Login successful", userId = user.Id, username = user.Username });
        }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}