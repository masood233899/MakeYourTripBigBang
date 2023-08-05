using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class PackageDetailsMaster
{
    public int Id { get; set; }

    public int? PackageId { get; set; }

    public int? PlaceId { get; set; }

    public int? DayNumber { get; set; }

    public string? PlaceImages { get; set; }

    public string? Iterinary { get; set; }

    public virtual PackageMaster? Package { get; set; }

    public virtual PlaceMaster? Place { get; set; }
}
