using DbEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class CartItemVM
    {
        public int cartItemId { get; set; }
        public int? CartId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
    public class CartVM
    {
        public int CartId { get; set; }
        public int LoginId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Quantity { get; set; }

    }
    public class CartCreateVM
    {
        public int LoginId { get; set; }
        public double TotalPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class CartUpdateVM
    {
        public int CartId { get; set; }
        public int LoginId { get; set; }
        public double TotalPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CartItemUpdateVM> CartItems { get; set; } = new List<CartItemUpdateVM>();
    }
    public class CartDeleteVM
    {
        public int CartId { get; set; }
    }
    public class CartItemCreateVM
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class CartItemUpdateVM
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class CartItemDeleteVM
    {
        public int CartItemId { get; set; }
    }
}
