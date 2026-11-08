using System;
using System.Collections.Generic;

namespace DbEntities.Entities;

public partial class Contact
{
    public int ContactId { get; set; }

    public int? LoginId { get; set; }

    public string ContactName { get; set; } = null!;

    public string ContactMessage { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Login? Login { get; set; }
}
