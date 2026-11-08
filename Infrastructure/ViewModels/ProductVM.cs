using Microsoft.AspNetCore.Http;
using My.Project.Areas.Admin.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.ViewModels
{
    public class ProductVM
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string ProductImage { get; set; } = null!;

        public string ProductDiscription { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsActive { get; set; }
        public string? Category { get; set; }
    }
    public class ProductCreateVM
    {
        public ProductCreateVM()
        {
            Categories = new List<LookupVM>();
        }
        [Required]
        public string ProductName { get; set; } = null!;
        [Required]
        public IFormFile ProductImage { get; set; } = null!;
        public string? ImageName { get; set; } = null!;
        [Required]
        public string ProductDiscription { get; set;} = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public List<LookupVM> Categories { get; set; }
    }
    public class ProductUpdateVM
    {
        public ProductUpdateVM()
        {
            Categories = new List<LookupVM>();
        }
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        public string? PictureName { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ProductImageName { get; set; }
        public IFormFile? ProductImage { get; set; } = null!;
        public string? ImageName { get; set; }
        public string ProductDiscription { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int? CategoryId { get; set; }
        public List<LookupVM> Categories { get; set; }
    }
}
