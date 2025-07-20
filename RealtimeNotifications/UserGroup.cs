using System;
using System.Collections.Generic;

namespace RealtimeNotifications;

public partial class UserGroup
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int UserGroupId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Group UserGroupNavigation { get; set; } = null!;
}
