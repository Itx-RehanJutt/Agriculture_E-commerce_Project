using System;
using System.Collections.Generic;

namespace DbEntities.Entities;

public partial class Order
{
    public int? CustomerId { get; set; }

    public int? ProductId { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }

    public int OrderQuantity { get; set; }

    public decimal TotalPrice { get; set; }

    public int OrderId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
