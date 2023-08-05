using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class VehicleDetailsMaster
{
    public int Id { get; set; }

    public int? VehicleId { get; set; }

    public decimal? CarPrice { get; set; }

    public int? PlaceId { get; set; }

    public string? VehicleImages { get; set; }

    public virtual PlaceMaster? Place { get; set; }

    public virtual VehicleMaster? Vehicle { get; set; }

    public virtual ICollection<VehicleBooking> VehicleBookings { get; set; } = new List<VehicleBooking>();
}
