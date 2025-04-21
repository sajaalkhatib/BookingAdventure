namespace BookingAdventure.Server.Models
{
    public class AdventureViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Level { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public int MaxParticipants { get; set; }
        public int InstructorId { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public int DestinationId { get; set; }

        //public List<IFormFile> Images { get; set; }
        // إضافة حقل URL الصورة
        public string ImageUrl { get; set; } // الآن يستقبل رابط صورة واحدة فقط بدلاً من List<string>
    }

}
