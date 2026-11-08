using System;
using System.Collections.Generic;

namespace DbEntities.Entities;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();
}
