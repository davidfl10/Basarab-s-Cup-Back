using finalexam_back.Data;
using finalexam_back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace finalexam_back.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : Controller
    {
        private readonly DataContext _context;

        public TeamController(DataContext context)
        {
            this._context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTeam([FromBody] Team team)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await this._context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    var newTeam = new Team
                    {
                        Name = team.Name,
                        Player1 = team.Player1,
                        Player2 = team.Player2,
                        Player3 = team.Player3,
                        Player4 = team.Player4,
                        Player5 = team.Player5,
                        Player6 = team.Player6,
                        Player7 = team.Player7,
                        Player8 = team.Player8,
                        Player9 = team.Player9,
                        Player10 = team.Player10,
                        Player11 = team.Player11,
                        Player12 = team.Player12,
                        Player13 = team.Player13,
                        Player14 = team.Player14,
                        UserId = (int)user.Id
                    };

                    _context.Teams.Add(newTeam);
                    await this._context.SaveChangesAsync();

                    user.TeamCode = newTeam.Id.ToString();
                    this._context.Update(user);
                    await this._context.SaveChangesAsync();

                    return Ok(newTeam);
                }
                

            } else
            {
                return BadRequest();
            }
            
        }
    }
}
