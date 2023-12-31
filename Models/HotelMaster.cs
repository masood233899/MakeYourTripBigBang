﻿using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class HotelMaster
{
    public int Id { get; set; }

    public string? HotelName { get; set; }

    public int? PlaceId { get; set; }

    public string? HotelImages { get; set; }

    public virtual PlaceMaster? Place { get; set; }

    public virtual ICollection<RoomDetailsMaster> RoomDetailsMasters { get; set; } = new List<RoomDetailsMaster>();
}
