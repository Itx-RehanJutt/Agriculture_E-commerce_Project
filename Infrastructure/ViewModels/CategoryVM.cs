using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
    }
    public class CreateCategoryVM
    {
        [Required(ErrorMessage = "Category Name Is Required")]
        public string? CategoryName { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
    public class UpdateCategoryVM
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name Is Required")]
        public string? CategoryName { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
