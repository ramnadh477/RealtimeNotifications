﻿using System;
using System.Collections.Generic;

namespace RealtimeNotifications.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
}
