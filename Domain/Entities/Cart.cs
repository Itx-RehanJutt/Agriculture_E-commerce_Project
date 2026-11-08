using System;
using System.Collections.Generic;

namespace DbEntities.Entities;

public partial class Cart
{
    public int CartId { get; set; }

    public int LoginId { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Login Login { get; set; } = null!;
}
