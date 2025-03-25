using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LetGoNowApi.Models;

namespace LetGoNowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly LetGoNowDbContext _context;

        public BookingsController(LetGoNowDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings
                .Include(b => b.Cruise)
                .Include(b => b.Hotel)
                .Include(b => b.Flight)
                .ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Cruise)
                .Include(b => b.Hotel)
                .Include(b => b.Flight)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null) return NotFound();
            return booking;
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(BookingDto bookingDto)
        {
            var booking = new Booking
            {
                CustomerName = bookingDto.CustomerName,
                CustomerEmail = bookingDto.CustomerEmail,
                CustomerPhone = bookingDto.CustomerPhone,
                CruiseId = bookingDto.CruiseId,
                HotelId = bookingDto.HotelId,
                FlightId = bookingDto.FlightId,
                StartDate = bookingDto.StartDate,
                EndDate = bookingDto.EndDate,
                TotalPrice = bookingDto.TotalPrice,
                Status = "Pending"
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Tải lại booking với dữ liệu liên quan
            var createdBooking = await _context.Bookings
                .Include(b => b.Cruise)
                .Include(b => b.Hotel)
                .Include(b => b.Flight)
                .FirstOrDefaultAsync(b => b.Id == booking.Id);

            return CreatedAtAction(nameof(GetBooking), new { id = createdBooking.Id }, createdBooking);
        }
    }

    public class BookingDto
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public int? CruiseId { get; set; }
        public int? HotelId { get; set; }
        public int? FlightId { get; set; }
        public DateOnly StartDate { get; set; } // Đổi từ DateTime sang DateOnly
        public DateOnly EndDate { get; set; }   // Đổi từ DateTime sang DateOnly
        public decimal TotalPrice { get; set; }
    }
}