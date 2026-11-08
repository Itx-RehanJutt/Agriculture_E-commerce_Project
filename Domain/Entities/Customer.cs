using System;
using System.Collections.Generic;

namespace DbEntities.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int? LoginId { get; set; }

    public string? Image { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();

    public virtual Login? Login { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
