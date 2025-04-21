using System;
using System.Collections.Generic;

namespace BookingAdventure.Server.Models;

public partial class Destination
{
    public int DestinationId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Adventure> Adventures { get; set; } = new List<Adventure>();

    public virtual ICollection<AdventureCategory> Categories { get; set; } = new List<AdventureCategory>();
}
