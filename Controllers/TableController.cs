using finalexam_back.Data;
using finalexam_back.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finalexam_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly DataContext _context;

        public TableController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Team>>> GetTeams()
        {
            var teams = await this._context.Teams.ToListAsync();

            return Ok(teams);
        }
    }
}
