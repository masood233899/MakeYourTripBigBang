using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class PackageMaster
{
    public int Id { get; set; }

    public decimal? PackagePrice { get; set; }

    public string? PackageName { get; set; }

    public int? TravelAgentId { get; set; }

    public string? Region { get; set; }

    public string? PackageImages { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<PackageDetailsMaster> PackageDetailsMasters { get; set; } = new List<PackageDetailsMaster>();

    public virtual User? TravelAgent { get; set; }
}
