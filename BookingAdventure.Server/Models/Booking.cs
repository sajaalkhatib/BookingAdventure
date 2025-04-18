using System;
using System.Collections.Generic;

namespace BookingAdventure.Server.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? AdventureId { get; set; }

    public DateTime? BookingDate { get; set; }

    public DateTime? ScheduledDate { get; set; }

    public int? NumberOfParticipants { get; set; }

    public string? Status { get; set; }

    public string? PaymentType { get; set; }

    public virtual Adventure? Adventure { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual User? User { get; set; }
}
