using BookingAdventure.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookingAdventure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DashboardController(MyDbContext context)
        {
            _context = context;
        }

        // Get Bookings
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await (from booking in _context.Bookings
                                  join user in _context.Users
                                  on booking.UserId equals user.UserId
                                  select new
                                  {
                                      BookingId = booking.BookingId,
                                      FullName = user.FullName,
                                      Email = user.Email,
                                      AdventureId = booking.AdventureId,
                                      BookingDate = booking.BookingDate,
                                      ScheduledDate = booking.ScheduledDate,
                                      NumberOfParticipants = booking.NumberOfParticipants,
                                      Status = booking.Status,
                                      PaymentType = booking.PaymentType
                                  }).ToListAsync();

            return Ok(bookings);
        }

        // Total Users
        [HttpGet("total-users")]
        public async Task<int> GetTotalUsers()
        {
            return await _context.Users.CountAsync();
        }

        // Total Bookings
        [HttpGet("total-bookings")]
        public async Task<int> GetTotalBookings()
        {
            return await _context.Bookings.CountAsync();
        }

        // Total Instructors
        [HttpGet("total-instructors")]
        public async Task<int> GetTotalInstructors()
        {
            return await _context.Instructors.CountAsync();
        }

        // =========== Services Management ===========

        // Get All Services
        [HttpGet("services")]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
            return await _context.Services.ToListAsync();
        }

        // Add a New Service
        [HttpPost("services")]
        public async Task<IActionResult> AddService([FromBody] DTO.DTOAddServise adminSer)
        {
            if (adminSer == null)
                return BadRequest("Invalid Service Data");

            var dataser = new Service { Description = adminSer.Description, ImageUrl = adminSer.ImageUrl, Name = adminSer.Name, Price = adminSer.Price };
            _context.Services.Add(dataser);
            _context.SaveChanges();
            return Ok(adminSer);
        }

        // Update Existing Service
        [HttpPut("services/{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] Service updatedService)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound("Service not found");

            // Update fields
            service.Name = updatedService.Name;
            service.Description = updatedService.Description;
            service.Price = updatedService.Price;
            // أضف هنا أي فيلد آخر عندك

            await _context.SaveChangesAsync();

            return Ok(service);
        }

        // Delete Service
        [HttpDelete("services/{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound("Service not found");

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return Ok("Service deleted successfully");
        }
    }
}
