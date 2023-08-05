using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Name { get; set; }

    public byte[]? Hashkey { get; set; }

    public byte[]? Password { get; set; }

    public string? Role { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<PackageMaster> PackageMasters { get; set; } = new List<PackageMaster>();

    public virtual ICollection<PostGallery> PostGalleries { get; set; } = new List<PostGallery>();
}
