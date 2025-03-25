using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LetGoNowApi.Models;

namespace LetGoNowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CruisesController : ControllerBase
    {
        private readonly LetGoNowDbContext _context;

        public CruisesController(LetGoNowDbContext context)
        {
            _context = context;
        }

        // GET: api/Cruises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cruise>>> GetCruises()
        {
            return await _context.Cruises.ToListAsync();
        }

        // GET: api/Cruises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cruise>> GetCruise(int id)
        {
            var cruise = await _context.Cruises.FindAsync(id);
            if (cruise == null) return NotFound();
            return cruise;
        }

        // POST: api/Cruises
        [HttpPost]
        public async Task<ActionResult<Cruise>> CreateCruise(Cruise cruise)
        {
            _context.Cruises.Add(cruise);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCruise), new { id = cruise.Id }, cruise);
        }

        // PUT: api/Cruises/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCruise(int id, Cruise cruise)
        {
            if (id != cruise.Id) return BadRequest();
            _context.Entry(cruise).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Cruises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCruise(int id)
        {
            var cruise = await _context.Cruises.FindAsync(id);
            if (cruise == null) return NotFound();
            _context.Cruises.Remove(cruise);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}