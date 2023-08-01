﻿using System;
using System.Collections.Generic;

namespace MakeYourTrip.Models;

public partial class Request
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }
}
