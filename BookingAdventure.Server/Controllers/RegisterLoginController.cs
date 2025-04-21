using System.Collections.Concurrent;
//using System.Net.Mail;
using BookingAdventure.Server.DTOs;
using BookingAdventure.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using static BookingAdventure.Server.Controllers.RegisterLoginController;

namespace BookingAdventure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterLoginController : ControllerBase
    {
        private readonly MyDbContext _db;

        private static Dictionary<string, string> ResetCodeStore = new();
        public RegisterLoginController(MyDbContext db)
        {
            _db = db;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] register dto)
        {
            if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest("Email already registered.");
            }

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = dto.Password,
                Phone = dto.Phone,
                Img = dto.Img
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] Login dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null)
            {
                return NotFound("User not found or Incorrect password.");
            }


            return Ok(new
            {
                Message = "Login successful",
                User = new
                {
                    user.UserId,

                }
            });
        }


        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(new
            {
                user.FullName,
                user.Email,
                user.Phone,
                user.Img
            });
        }

        //[HttpPut("update-profile/{id}")]
        //public async Task<IActionResult> UpdateProfile(int id, [FromForm] editProfile dto)
        //{
        //    var user = await _db.Users.FindAsync(id);
        //    if (user == null)
        //        return NotFound("User not found.");

        //    user.FullName = dto.FullName;
        //    user.Phone = dto.Phone;

        //    if (dto.ImageFile != null && dto.ImageFile.Length > 0)
        //    {
        //        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        //        if (!Directory.Exists(uploadsFolder))
        //            Directory.CreateDirectory(uploadsFolder);

        //        // احفظ الصورة باسم فريد
        //        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
        //        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await dto.ImageFile.CopyToAsync(stream);
        //        }

        //        // خزّن الاسم فقط في الداتابيز
        //        user.Img = uniqueFileName;
        //    }

        //    await _db.SaveChangesAsync();
        //    return Ok(new { success = true, message = "Profile updated successfully." });
        //}


        [HttpPut("update-profile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromForm] editProfile dto)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            user.FullName = dto.FullName;
            user.Phone = dto.Phone;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // احفظ الصورة باسم فريد
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                // خزّن الاسم فقط في الداتابيز
                user.Img = uniqueFileName;
            }

            await _db.SaveChangesAsync();
            return Ok(new { success = true, message = "Profile updated successfully." });
        }





        //[HttpPut("edit-profile/{id}")]
        //public IActionResult EditProfileById(int id, [FromForm] editProfile dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var user = _db.Users.Find(id);
        //    if (user == null)
        //        return NotFound("User not found.");

        //    user.FullName = dto.FullName;
        //    user.Phone = dto.Phone;
        //    user.Img = dto.Img;

        //    _db.SaveChanges();

        //    return Ok("Profile updated successfully.");
        //}

        [HttpPut("change-password")]

        public async Task<IActionResult> ChangePassword([FromForm] resetPassword dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == dto.UserId);
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == 1);
            if (user == null)
                return NotFound();

            if (user.Password != dto.OldPassword)
                return BadRequest();

            if (dto.NewPassword != dto.ConfirmNewPassword)
                return BadRequest();

            user.Password = dto.NewPassword;

            await _db.SaveChangesAsync();

            //return Ok("Password changed successfully
            //
            return Ok(new { success = true, message = "Password changed successfully." });


        }
        public class ResetCodeRequestDTO
        {
            public string Email { get; set; }
        }

        [HttpPost("send-reset-code")]
        public async Task<IActionResult> SendResetCode([FromBody] ResetCodeRequestDTO request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email))
                    return BadRequest("Email is required");

                var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (user == null)
                    return BadRequest("User not found");

                var code = new Random().Next(100000, 999999).ToString();
                ResetCodeStore[request.Email] = code;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Tripfy", "tested4email@gmail.com"));
                message.To.Add(new MailboxAddress("", request.Email));
                message.Subject = "Password Reset Code";
                message.Body = new TextPart("plain")
                {
                    Text = $"Your password reset code is: {code}"
                };

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("tested4email@gmail.com", "jjde lbzz zdxy cvxs"); // استبدليه بقيمة آمنة
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return Ok(new { message = "Reset code sent successfully" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while sending reset code");
            }
        }

        [HttpPost("verify-reset-code")]
        public IActionResult VerifyResetCode([FromBody] VerifyCodeDTO request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Code))
                    return BadRequest("Email and code are required");

                bool isValid = ResetCodeStore.TryGetValue(request.Email, out var storedCode) && storedCode == request.Code;

                return isValid
                    ? Ok(new { message = "Code verified successfully" })
                    : BadRequest("Invalid verification code");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while verifying code");
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.NewPassword))
                    return BadRequest("Email and new password are required");

                var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (user == null)
                    return BadRequest("User not found");

                user.Password = request.NewPassword; // بدون تشفير

                await _db.SaveChangesAsync();

                // حذف الكود بعد الاستخدام
                ResetCodeStore.Remove(request.Email);

                return Ok(new { message = "Password reset successfully" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while resetting password");
            }
        }
    }

}

    

