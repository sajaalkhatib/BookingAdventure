using System;
using System.Collections.Generic;

namespace BookingAdventure.Server.Models;

public partial class AdventureType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Adventure> Adventures { get; set; } = new List<Adventure>();
}
