using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class RoomTypeMaster
{
    public int Id { get; set; }

    public string? RoomType { get; set; }

    public virtual ICollection<RoomDetailsMaster> RoomDetailsMasters { get; set; } = new List<RoomDetailsMaster>();
}
