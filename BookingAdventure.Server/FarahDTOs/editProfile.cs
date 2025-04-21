using Microsoft.AspNetCore.Mvc;

namespace BookingAdventure.Server.DTOs
{
    public class editProfile
    {
        public string? FullName { get; set; }


        public string? Phone { get; set; }

        [FromForm]
        public IFormFile ImageFile { get; set; }
    }
}
