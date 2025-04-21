using BookingAdventure.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingAdventure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly MyAppDbContext _context;

        public BookingController(MyAppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult CreateBooking([FromBody] Booking booking)
        {
            booking.BookingDate = DateTime.Now;
            booking.Status = "Pending";

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return Ok(booking);
        }



        [HttpGet("user/{UserId}")]
        public IActionResult GetUserBookings(int UserId)
        {
            var bookings = _context.Bookings
                .Where(b => b.UserId == UserId)
                .OrderByDescending(b => b.ScheduledDate)
                .ToList();

            return Ok(bookings);
        }




    }
}
