using BookingAdventure.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAdventure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {

        private readonly MyAppDbContext _context;

        public FeedbackController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
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
