using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class RoomBooking
{
    public int Id { get; set; }

    public int? RoomDetailsId { get; set; }

    public int? BookingId { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual RoomDetailsMaster? RoomDetails { get; set; }
}
