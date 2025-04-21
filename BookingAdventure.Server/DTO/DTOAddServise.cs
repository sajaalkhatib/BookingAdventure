using BookingAdventure.Server.Models;

namespace BookingAdventure.Server.DTO
{
    public class DTOAddServise
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public string? Level { get; set; }
        public decimal? Price { get; set; }
        public string? Location { get; set; }
        public int? MaxParticipants { get; set; }
        public bool? IsAvailable { get; set; }
        public string? ImageUrl { get; set; }


        public string? InstructorName { get; set; }
        public string? CategoryName { get; set; }
        public string? DestinationName { get; set; }


    }
}
