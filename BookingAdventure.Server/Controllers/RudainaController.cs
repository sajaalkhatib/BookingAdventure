using Microsoft.AspNetCore.Mvc;
using BookingAdventure.Server.Models;
using Microsoft.EntityFrameworkCore;
using BookingAdventure.Server.DTO_Rudaina;
namespace BookingAdventure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RudainaController : Controller
    {
        private readonly MyDbContext _context;
        public RudainaController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet("api/destinations")]
        public IActionResult GetAllDestinations()
        {
            var destinations = _context.Destinations.ToList();
            return Ok(destinations);
        }
        [HttpGet("allAdventures")]
        public async Task<IActionResult> GetAllAdventures()
        {
            var adventures = await _context.Adventures
                .Include(a => a.Instructor)
                .Include(a => a.Category)
                .Include(a => a.Destination)
                .Include(a => a.AdventureType)
                .Include(a => a.AdventureImages)
                .ToListAsync();

            var adventureDtos = adventures.Select(a => new AdventureDto
            {
                Id = a.AdventureId,
                Title = a.Title,
                Description = a.Description,
                TypeName = a.AdventureType?.TypeName,
                DestinationName = a.Destination?.Name,
                DestinationId = a.DestinationId,

                Images = a.AdventureImages.Select(img => img.ImageUrl).ToList()
            }).ToList();

            return Ok(adventureDtos);
        }
        // Action لعرض تفاصيل المغامرة مع تفاصيل AdventureDetails
        [HttpGet("GetAdventureDetails/{id}")]
        public async Task<IActionResult> GetAdventureDetails(int id)
        {
            var adventure = await _context.Adventures
                .Include(a => a.AdventureType)
                .Include(a => a.Destination)
                .Include(a => a.AdventureImages)
                .Include(a => a.AdventureDetails) // تأكد من تضمين AdventureDetails هنا
                .FirstOrDefaultAsync(a => a.AdventureId == id);

            if (adventure == null)
                return NotFound();

            var dto = new AdventureDto
            {
                Id = adventure.AdventureId,
                Title = adventure.Title,
                Description = adventure.Description,
                TypeName = adventure.AdventureType?.TypeName,
              Price = adventure?.Price,
                MaxParticipants = adventure?.MaxParticipants,
                Duration = adventure?.Duration,



                DestinationName = adventure.Destination?.Name,
                DestinationId = adventure.DestinationId,
                Images = adventure.AdventureImages.Select(img => img.ImageUrl).ToList(),
                Overview = adventure.AdventureDetails?.FirstOrDefault()?.Overview, // الحصول على أول عنصر في AdventureDetails
                HighlightsJson = adventure.AdventureDetails?.FirstOrDefault()?.HighlightsJson,  // الوصول لأول عنصر من AdventureDetails
                FaqsJson = adventure.AdventureDetails?.FirstOrDefault()?.FaqsJson
            };

            return Ok(dto);
        }
        [HttpPost ("CreateBooking") ]
        public IActionResult CreateBooking([FromBody] Booking booking)
        {
            booking.BookingDate = DateTime.Now;
            booking.Status = "Pending";

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return Ok(booking);
        }
        [HttpPost("CreateFeedback")]
        public IActionResult Create([FromBody] Review review)
        {

            review.ReviewDate = DateTime.Now;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return Ok(review);
        }
        [HttpGet("ByAdventure/{id}")]
        public IActionResult GetByAdventure(int id)
        {
            var reviews = _context.Reviews
                .Where(r => r.AdventureId == id)
                .OrderByDescending(r => r.ReviewDate)
                .ToList();

            return Ok(reviews);
        }
    }
}