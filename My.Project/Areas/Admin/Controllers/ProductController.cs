using Infrastructure.ViewModels;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace My.Project.Areas.Admin.Controllers
{
     [Area("Admin")]
    public class ProductController : BaseController
    {
        private ICMSRepository _iCMSRepository;
        private IWebHostEnvironment _webHostEnvironment;

        public ProductController( IWebHostEnvironment webHostEnvironment , ICMSRepository iCMSRepository)
        {
            _iCMSRepository = iCMSRepository;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Product(bool _isProductDelete = false)
        {
            var isProductDelete = TempData["isProductDeleted"];
            if (isProductDelete != null)
            {
                if (isProductDelete.ToString() == "1")
                {
                    ViewBag.isProductDelete = "1";
                }
                else
                {
                    ViewBag.isProductDelete = "0";
                }
            }
            return View(await _iCMSRepository.Product());
        }
        public async Task<IActionResult> ProductCreate()
        {
            var model = new ProductCreateVM()
            {
                Categories = await _iCMSRepository.CategoryGetAll()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductCreateVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProductImage != null)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ProductImage.FileName)}";
                    var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Product/Product_Pic");
                    var imgPath = Path.Combine(folderPath, fileName);
                    using (var stream = new FileStream(imgPath, FileMode.Create))
                    {
                        await model.ProductImage.CopyToAsync(stream);
                    }
                    model.ImageName = fileName;
                }
                _iCMSRepository.AddProduct(model);
                ModelState.AddModelError("", "Product created successfully");
            }
            var viewmodel = new ProductCreateVM()
            {
                Categories = await _iCMSRepository.CategoryGetAll()
            };
            return RedirectToAction("Product", new { Controller = "Product", Area = "Admin" });
        }
        public async Task<IActionResult> ProductEdit(int id)
        {
            return View(await _iCMSRepository.ProductGetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProductImage != null)
                {
                   var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ProductImage.FileName)}";
                   var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Product/Product_Pic");
                   var imgPath = Path.Combine(folderPath, fileName);
                   using (var stream = new FileStream(imgPath, FileMode.Create))
                   {
                       await model.ProductImage.CopyToAsync(stream);
                   }
                   model.ImageName = fileName;
                }
                await _iCMSRepository.ProductUpdate(model);
                ModelState.AddModelError("", "Product updated successfully");
            }
            return View(await _iCMSRepository.ProductGetById(model.Id));
        }
        public async Task<IActionResult> ProductDelete(int id)
        {
            var result = await _iCMSRepository.ProductDelete(id);
            TempData["isGuardDeleted"] = result.ToString();
            return RedirectToAction("Product", new { Controller = "Product", Area = "Admin" });
        }
    }    
}
