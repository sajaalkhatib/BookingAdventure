using System;
using System.Collections.Generic;

namespace BookingAdventure.Server.Models;

public partial class ContactMessage
{
    public int MessageId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Message { get; set; }

    public DateTime? DateSent { get; set; }
}
