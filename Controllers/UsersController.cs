using finalexam_back.Data;
using finalexam_back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace finalexam_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser()
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await this._context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditUser(User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await this._context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Phone = updatedUser.Phone;
            user.TeamCode = updatedUser.TeamCode;

            if ( updatedUser.Password.Length > 8 )
            {
                user.Password = updatedUser.Password;
            }

            await this._context.SaveChangesAsync();
            return Ok(user);
        }
    }
}
