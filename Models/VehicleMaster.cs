using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class VehicleMaster
{
    public int Id { get; set; }

    public string? VehicleName { get; set; }

    public int? NumberOfSeats { get; set; }

    public virtual ICollection<VehicleDetailsMaster> VehicleDetailsMasters { get; set; } = new List<VehicleDetailsMaster>();
}
