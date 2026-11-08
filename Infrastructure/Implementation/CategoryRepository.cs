using DbEntities.Entities;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private AgricultureProjectContext _context;

        public CategoryRepository(AgricultureProjectContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<CategoryVM>> Category()
        {
            var model = new List<CategoryVM>();
            var categories = await _context.Categories.Where(p => p.IsDeleted != true).ToListAsync();
            if (categories.Any())
            {
                foreach (var item in categories)
                {
                    model.Add(new CategoryVM()
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        CreateDate = item.CreatedDate,
                        IsActive = item.IsActive,
                        IsDeleted = item.IsDeleted,
                    });
                }
            }
            return model;
        }
        public async Task AddCategory(CreateCategoryVM model)
        {
            var categories = new Category()
            {
                CategoryName = model.CategoryName,
                IsActive = model.IsActive,
                IsDeleted = false,
            };
            await _context.AddAsync(categories);
            await _context.SaveChangesAsync();
        }
        public async Task<UpdateCategoryVM> CategoryGetById(int Id)
        {
            var category = await _context.Categories.Where(p => p.CategoryId == Id && p.IsDeleted == false).FirstOrDefaultAsync();
            if (category == null)
            {
                return null; // or throw an exception, depending on your requirements
            }
            var model = new UpdateCategoryVM()
            {
                CategoryId = Id,
                CategoryName = category.CategoryName,
                IsActive = category.IsActive,
            };
            return model;
        }
        public async Task<bool> UpdateCategory(UpdateCategoryVM model)
        {
            var category = await _context.Categories.Where(p => p.CategoryId == model.CategoryId && p.IsDeleted == false).FirstOrDefaultAsync();
            if (category == null)
            {
                return false;
            }
            category.CategoryId = model.CategoryId;
            category.CategoryName = model.CategoryName;
            category.IsActive = model.IsActive;
            _context.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<int> DeleteCategory(int Id)
        {
            var category = await _context.Categories.Where(p => p.CategoryId == Id && p.IsDeleted == false).FirstOrDefaultAsync();
            if (category != null)
            {
                category.IsDeleted = true;
                _context.Update(category);
                await _context.SaveChangesAsync();
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
