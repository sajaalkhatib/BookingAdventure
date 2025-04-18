using System;
using System.Collections.Generic;

namespace BookingAdventure.Server.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? AdventureId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? ReviewDate { get; set; }

    public virtual Adventure? Adventure { get; set; }
}
