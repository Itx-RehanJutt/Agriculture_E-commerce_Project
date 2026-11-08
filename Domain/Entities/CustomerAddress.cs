using System;
using System.Collections.Generic;

namespace DbEntities.Entities;

public partial class CustomerAddress
{
    public int AddressId { get; set; }

    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
