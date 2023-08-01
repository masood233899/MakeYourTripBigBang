using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? PackageMasterId { get; set; }

    public string? Feedback { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual PackageMaster? PackageMaster { get; set; }

    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();

    public virtual User? User { get; set; }

    public virtual ICollection<VehicleBooking> VehicleBookings { get; set; } = new List<VehicleBooking>();
}
