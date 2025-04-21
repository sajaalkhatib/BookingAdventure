using System;
using System.Collections.Generic;

namespace BookingAdventure.Server.Models;

public partial class AdventureDetail
{
    public int Id { get; set; }

    public int AdventureId { get; set; }

    public string Overview { get; set; } = null!;

    public string? HighlightsJson { get; set; }

    public string? FaqsJson { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Adventure Adventure { get; set; } = null!;
}
