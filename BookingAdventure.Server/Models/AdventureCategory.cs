using System;
using System.Collections.Generic;

namespace BookingAdventure.Server.Models;

public partial class AdventureCategory
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Adventure> Adventures { get; set; } = new List<Adventure>();

    public virtual ICollection<Destination> Destinations { get; set; } = new List<Destination>();
}
