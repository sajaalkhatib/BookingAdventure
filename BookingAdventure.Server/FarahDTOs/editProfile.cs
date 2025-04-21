using Microsoft.AspNetCore.Http;

namespace BookingAdventure.Server.DTOs
{
    public class editProfile
    {
        public string? FullName { get; set; }

        public string? Phone { get; set; }

        public IFormFile? ImageFile { get; set; } // ✅ بدون [FromForm] + Nullable
    }
}
