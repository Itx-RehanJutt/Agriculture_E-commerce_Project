using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class AddressVM
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? OrderAddress { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
    }
    public class AddressCreateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "City Name is Required")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string? OrderAddress { get; set; }
        [Required (ErrorMessage = "Phone Number is required")]
        public string? PhoneNo { get;set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int CustomerID { get; set; }
    }
    public class AddressUpdateVM
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string? PhoneNo { get; set; }
        [Required(ErrorMessage = "City Name is Required")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string? OrderAddress { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int? CustomerId { get; set; }
    }

}
