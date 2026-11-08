using System;
using System.Collections.Generic;

namespace DbEntities.Entities;

public partial class Login
{
    public int LoginId { get; set; }

    public string Email { get; set; } = null!;

    public int? RoleId { get; set; }

    public string UserName { get; set; } = null!;

    public string? LoginImage { get; set; }

    public string LoginPassword { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual UserRole? Role { get; set; }
}
