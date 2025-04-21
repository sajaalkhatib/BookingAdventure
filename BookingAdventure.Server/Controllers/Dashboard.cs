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
        [HttpGet("bookings")] // <-- عدلنا هون
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

        [HttpGet("adventures")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllAdventures()
        {
            var adventures = await _context.Adventures
                .Include(a => a.AdventureImages)
                .Include(a => a.Instructor)
                .Include(a => a.Destination)
                .Include(a => a.Category)
                .Select(a => new
                {
                    a.AdventureId,
                    a.Title,
                    a.Description,
                    a.Duration,
                    a.Level,
                    a.Price,
                    a.Location,
                    a.MaxParticipants,
                    a.IsAvailable,
                    //a.InstructorName = a.Instructor.FullName,
                    //a.CategoryName = a.Category.CategoryName,
                    //a.DestinationName = a.Destination.Name,
                    Images = a.AdventureImages.Select(img => new { img.ImageUrl }).ToList()
                })
                .ToListAsync();

            return Ok(adventures);
        }

        // Add Adventure
        [HttpPost("adventure")]
        public async Task<IActionResult> AddService([FromBody] DTO.DTOAddServise adminSer)
        {
            if (adminSer == null)
            {
                return BadRequest("Invalid Service Data");
            }

            Console.WriteLine("Received Adventure: " + adminSer.Title); // تحقق من البيانات التي تستلمها

            var adventure = new Adventure
            {
                Title = adminSer.Title,
                Description = adminSer.Description,
                Duration = adminSer.Duration,
                Level = adminSer.Level,
                Price = adminSer.Price,
                Location = adminSer.Location,
                MaxParticipants = adminSer.MaxParticipants,
                IsAvailable = adminSer.IsAvailable,
            };

            _context.Adventures.Add(adventure);
            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(adminSer.ImageUrl))
            {
                var adventureImage = new AdventureImage
                {
                    AdventureId = adventure.AdventureId,
                    ImageUrl = adminSer.ImageUrl
                };
                _context.AdventureImages.Add(adventureImage);
                await _context.SaveChangesAsync();
            }

            return Ok(adminSer);
        }


        [HttpPut("adventure/{id}")]
        public async Task<IActionResult> UpdateAdventure(int id, [FromBody] DTO.DTOAddServise updatedAdventure)
        {
            var adventure = await _context.Adventures
                .Include(a => a.AdventureImages)
                .FirstOrDefaultAsync(a => a.AdventureId == id);

            if (adventure == null)
                return NotFound("Adventure not found");

            adventure.Title = updatedAdventure.Title;
            adventure.Description = updatedAdventure.Description;
            adventure.Duration = updatedAdventure.Duration;
            adventure.Level = updatedAdventure.Level;
            adventure.Price = updatedAdventure.Price;
            adventure.Location = updatedAdventure.Location;
            adventure.MaxParticipants = updatedAdventure.MaxParticipants;
            adventure.IsAvailable = updatedAdventure.IsAvailable;

            if (!string.IsNullOrEmpty(updatedAdventure.ImageUrl))
            {
                var existingImage = adventure.AdventureImages.FirstOrDefault();
                if (existingImage != null)
                {
                    existingImage.ImageUrl = updatedAdventure.ImageUrl;
                }
                else
                {
                    var newImage = new AdventureImage
                    {
                        AdventureId = adventure.AdventureId,
                        ImageUrl = updatedAdventure.ImageUrl
                    };
                    _context.AdventureImages.Add(newImage);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                adventure.AdventureId,
                adventure.Title,
                adventure.Description,
                adventure.Duration,
                adventure.Level,
                adventure.Price,
                adventure.Location,
                adventure.MaxParticipants,
                adventure.IsAvailable,
                Images = adventure.AdventureImages.Select(img => new { img.ImageUrl }).ToList()
            });
        }

        [HttpDelete("adventure/{id}")]
        public async Task<IActionResult> DeleteAdventure(int id)
        {
            // البحث عن المغامرة مع الحجز والصور المرتبطة
            var adventure = await _context.Adventures
                .Include(a => a.AdventureImages)  // تحميل الصور المرتبطة
                .Include(a => a.Bookings)         // تحميل الحجز المرتبط
                .FirstOrDefaultAsync(a => a.AdventureId == id);

            // إذا لم يتم العثور على المغامرة
            if (adventure == null)
                return NotFound("Adventure not found");

            // حذف الحجز المرتبط بالمغامرة أولاً
            _context.Bookings.RemoveRange(adventure.Bookings);

            // حذف الصور المرتبطة
            _context.AdventureImages.RemoveRange(adventure.AdventureImages);

            // الآن حذف المغامرة نفسها
            _context.Adventures.Remove(adventure);

            // حفظ التغييرات في قاعدة البيانات
            await _context.SaveChangesAsync();

            return Ok();
        }



    }
}
