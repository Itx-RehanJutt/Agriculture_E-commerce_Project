using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace My.Project.ViewComponents
{
    public class IndexViewComponent : ViewComponent
    {
        private ICMSRepository _cMSRepository;

        public IndexViewComponent(ICMSRepository cMSRepository)
        {
            _cMSRepository = cMSRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _cMSRepository.Product();
            var limitedProducts = products.Take(6).ToList();
            return View("Index", limitedProducts);
        }

    }
}
