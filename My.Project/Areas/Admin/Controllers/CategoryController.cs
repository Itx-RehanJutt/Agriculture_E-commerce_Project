using Infrastructure.Implementation;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My.Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : BaseController
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Category(bool _isGuardDelete = false)
        {
            var isCategoryDelete = TempData["isCategoryDelete"];
            if (isCategoryDelete != null)
            {
                if (isCategoryDelete.ToString() == "1")
                {
                    ViewBag.isCategoryDelete = "1";
                }
                else
                {
                    ViewBag.isCategoryDelete = "0";
                }
            }
            return View(await _categoryRepository.Category());
        }
        public IActionResult CreateCategory() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.AddCategory(model);
                ModelState.AddModelError("", "Category created successfully");
                return RedirectToAction("Category" , "Category");
            }
            return View();
        }
        public async Task<IActionResult> UpdateCategory(int id)
        {
            if (ModelState.IsValid)
            {
                return View(await _categoryRepository.CategoryGetById(id));
            }
            ModelState.AddModelError("", "Category Not Found.");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.UpdateCategory(model);
                ModelState.AddModelError("", "Category Updated successfully");
                return RedirectToAction("Category", "Category");

            }
            ModelState.AddModelError("", "Incorrect Data Please Try Again.");
            return View(await _categoryRepository.CategoryGetById(model.CategoryId));
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryRepository.DeleteCategory(id);
            TempData["isCategoryDelete"] = result.ToString();
            return RedirectToAction("Category" , new { Controller = "Category" , Area = "Admin" });
        }
    }
}
