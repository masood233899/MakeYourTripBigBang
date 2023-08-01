using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class VehicleBooking
{
    public int Id { get; set; }

    public int? VehicleDetailsId { get; set; }

    public int? BookingId { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual VehicleDetailsMaster? VehicleDetails { get; set; }
}
