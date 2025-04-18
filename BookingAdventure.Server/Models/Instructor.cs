using System;
using System.Collections.Generic;

namespace BookingAdventure.Server.Models;

public partial class Instructor
{
    public int InstructorId { get; set; }

    public string? FullName { get; set; }

    public string? Bio { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Adventure> Adventures { get; set; } = new List<Adventure>();
}
