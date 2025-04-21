namespace BookingAdventure.Server.DTO_Rudaina
{
    public class AdventureDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Duration { get; set; }
        public int? MaxParticipants { get; set; }

        public string TypeName { get; set; }
        public string DestinationName { get; set; }
        public int? DestinationId { get; set; }  // أضفنا هذا

        public List<string> Images { get; set; }

        public string Overview { get; set; } = null!;

        public string? HighlightsJson { get; set; }

        public string? FaqsJson { get; set; }
    }
}
