using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class RoomDetailsMaster
{
    public int Id { get; set; }

    public decimal? Price { get; set; }

    public int? RoomTypeId { get; set; }

    public int? HotelId { get; set; }

    public string? Description { get; set; }

    public virtual HotelMaster? Hotel { get; set; }

    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();

    public virtual RoomTypeMaster? RoomType { get; set; }
}
