using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LetGoNowApi.Models;

namespace LetGoNowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly LetGoNowDbContext _context;

        public FlightsController(LetGoNowDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            return await _context.Flights.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return NotFound();
            return flight;
        }

        [HttpPost]
        public async Task<ActionResult<Flight>> CreateFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFlight), new { id = flight.Id }, flight);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, Flight flight)
        {
            if (id != flight.Id) return BadRequest();
            _context.Entry(flight).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return NotFound();
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}