using System;
using System.Collections.Generic;

namespace BookingAdventure.Server.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
