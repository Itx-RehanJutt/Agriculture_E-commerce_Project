using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace My.Project.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private ICMSRepository _cMSRepository;
        public ProductViewComponent(ICMSRepository cMSRepository)
        {
            _cMSRepository = cMSRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ID = Convert.ToInt32(ViewBag.Id);
            if (ID == null)
            {
                throw new Exception("Id not found");
            }
            ProductVM model = await _cMSRepository.ProductDetailsGetById(ID);
            return View("Product" , model);
        }
    }
}
