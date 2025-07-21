using System;
using System.Collections.Generic;

namespace RealtimeNotifications;

public partial class Group
{
    public int GrupeId { get; set; }

    public string? GroupName { get; set; }

    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}
