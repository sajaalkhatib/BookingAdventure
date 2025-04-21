namespace BookingAdventure.Server.Models
{
    public class AdventureDetailsDto
    {
        
            public int AdventureId { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public int? Duration { get; set; }
            public decimal? Price { get; set; }
            public int? MaxParticipants { get; set; }
            public string? AdventureTypeName { get; set; }
            public string? InstructorName { get; set; }
            public List<string> images { get; set; } = new();
        }
}
