using DbEntities.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
    public class SignInResponseVM
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? RoleId { get; set; }
        public int? CustomerId { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? LoginImage { get; set; }
        public virtual UserRole? Role { get; set; } = null!;
    }
    public class SignUpVM
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "PhoneNo is required")]
        public string PhoneNo { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }

    }
}
