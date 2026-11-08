using Infrastructure.ViewModels;
using My.Project.Areas.Admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddCategory(CreateCategoryVM model);
        Task<IEnumerable<CategoryVM>> Category();
        Task<UpdateCategoryVM> CategoryGetById(int Id);
        Task<bool> UpdateCategory(UpdateCategoryVM model);
        Task<int> DeleteCategory(int Id);
    }
}
