using System;
using System.Collections.Generic;

namespace BookingAdventure.Server.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Phone { get; set; }

    public string? Img { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
