using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class PlaceMaster
{
    public int Id { get; set; }

    public string? PlaceName { get; set; }

    public virtual ICollection<HotelMaster> HotelMasters { get; set; } = new List<HotelMaster>();

    public virtual ICollection<PackageDetailsMaster> PackageDetailsMasters { get; set; } = new List<PackageDetailsMaster>();

    public virtual ICollection<VehicleDetailsMaster> VehicleDetailsMasters { get; set; } = new List<VehicleDetailsMaster>();
}
