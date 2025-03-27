using Microsoft.AspNetCore.Mvc;
using Supabase;
using LetGoNowApi.Models;

namespace LetGoNowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Supabase.Client _supabaseClient;

        public AuthController(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                // Đăng nhập bằng email và mật khẩu
                var session = await _supabaseClient.Auth.SignIn(model.Email, model.Password);
                if (session == null)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                // Kiểm tra xem user có vai trò admin không
                var admin = await _supabaseClient.From<Admin>()
                    .Where(x => x.Email == model.Email)
                    .Single();

                if (admin == null || admin.Role != "admin")
                {
                    return Unauthorized(new { message = "User is not an admin" });
                }

                return Ok(new { token = session.AccessToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}