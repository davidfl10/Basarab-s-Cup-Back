using finalexam_back.Data;
using finalexam_back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace finalexam_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this._context.Users.Add(user);
            await this._context.SaveChangesAsync();

            return Ok(user);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(User login)
        {
            var user = await this._context.Users.SingleOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token });

        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
