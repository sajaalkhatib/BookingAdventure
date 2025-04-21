using Microsoft.AspNetCore.Mvc;
using BookingAdventure.Server.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace BookingAdventure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RudainaAdminController : ControllerBase
    {
        private readonly MyAppDbContext _DB;
        private readonly IWebHostEnvironment _env;

        public RudainaAdminController(MyAppDbContext DB, IWebHostEnvironment env)
        {
            _DB = DB;
            _env = env;
        }

        [HttpPost("AddAdventure")]
        public async Task<IActionResult> AddAdventure([FromForm] AdventureViewModel model)
        {
            if (model == null)
                return BadRequest("Invalid data.");

            // إنشاء المغامرة
            var adventure = new Adventure
            {
                Title = model.Title,
                Description = model.Description,
                Duration = model.Duration,
                Level = model.Level,
                Price = model.Price,
                Location = model.Location,
                InstructorId = model.InstructorId,
                MaxParticipants = model.MaxParticipants,
                DestinationId = model.DestinationId,
                AdventureTypeId = model.TypeId,
                AdventureImages = new List<AdventureImage>()
            };

            // ✅ معالجة أكثر من رابط صورة مفصول بفواصل أو أسطر جديدة
            if (!string.IsNullOrEmpty(model.ImageUrl))
            {
                var urls = model.ImageUrl
                    .Split(new[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(url => url.Trim())
                    .Where(url => Uri.IsWellFormedUriString(url, UriKind.Absolute) && url.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase));

                foreach (var url in urls)
                {
                    adventure.AdventureImages.Add(new AdventureImage { ImageUrl = url });
                }

                if (!adventure.AdventureImages.Any())
                {
                    return BadRequest("No valid image URLs provided.");
                }
            }

            // حفظ في قاعدة البيانات
            _DB.Adventures.Add(adventure);
            await _DB.SaveChangesAsync();

            return Ok(new { message = "Adventure added successfully", adventure });
        }

        [HttpGet("unique-destinations")]
        public IActionResult GetUniqueDestinations()
        {
            var destinations = _DB.Destinations
                .Select(d => new { d.DestinationId, d.Name }) // تضمين المعرف مع الاسم
                .Distinct()
                .ToList();

            return Ok(destinations);
        }

        [HttpGet("unique-adventure-types")]
        public IActionResult GetUniqueAdventureTypes()
        {
            var types = _DB.AdventureTypes
                .Select(t => new { t.TypeId, t.TypeName }) // تضمين المعرف مع الاسم
                .Distinct()
                .ToList();

            return Ok(types);
        }

        [HttpGet("unique-instructors")]
        public IActionResult GetUniqueInstructors()
        {
            var instructors = _DB.Instructors
                .Select(i => new { i.InstructorId, i.FullName }) // تضمين المعرف مع الاسم
                .Distinct()
                .ToList();

            return Ok(instructors);
        }




    }
}
