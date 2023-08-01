using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class PackageDetailsMaster
{
    public int Id { get; set; }

    public int? PackageId { get; set; }

    public int? PlaceId { get; set; }

    public string? DayNumber { get; set; }

    public virtual PackageMaster? Package { get; set; }

    public virtual PlaceMaster? Place { get; set; }
}
