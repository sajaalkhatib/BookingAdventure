using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookingAdventure.Server.Models;

public partial class AdventureImage
{
    public int ImageId { get; set; }

    public int? AdventureId { get; set; }

    public string? ImageUrl { get; set; }
    [JsonIgnore]

    public virtual Adventure? Adventure { get; set; }
}
