using System;
using System.Collections.Generic;

namespace BookingAdventure.Server.Models;

public partial class AdventureCategory
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Adventure> Adventures { get; set; } = new List<Adventure>();
}
