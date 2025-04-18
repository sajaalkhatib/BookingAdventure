using BookingAdventure.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingAdventure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterLoginController : ControllerBase
    {
        private readonly MyDbContext _db;

        public RegisterLoginController(MyDbContext db)
        {
            _db = db;
        }


        //[HttpPost("register")]
        //public IActionResult Register(RegisterDto dto)
        //{
        //    // تحقق إذا الإيميل مستخدم
        //    if ( _db.Users.AnyAsync(u => u.Email == dto.Email))
        //    {
        //        return BadRequest("Email is already registered.");
        //    }

        //    var user = new ApplicationUser
        //    {
        //        FullName = dto.FullName,
        //        Email = dto.Email,
        //        Password = dto.Password 
        //    };

        //    _db.Users.Add(user);
        //    _db.SaveChangesAsync();

        //    return Ok("User registered successfully.");
        //}
    }
}
