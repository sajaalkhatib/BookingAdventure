using System;
using System.Collections.Generic;

namespace BookingAdventure.Server.Models;

public partial class Adventure
{
    public int AdventureId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? Duration { get; set; }

    public string? Level { get; set; }

    public decimal? Price { get; set; }

    public string? Location { get; set; }

    public int? InstructorId { get; set; }

    public int? CategoryId { get; set; }

    public int? MaxParticipants { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual ICollection<AdventureImage> AdventureImages { get; set; } = new List<AdventureImage>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual AdventureCategory? Category { get; set; }

    public virtual Instructor? Instructor { get; set; }
}
