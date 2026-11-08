using Infrastructure.Implementation;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace My.Project.ViewComponents
{
    public class ShopViewComponent : ViewComponent
    {
        private ICMSRepository _cMSRepository;

        public ShopViewComponent(ICMSRepository cMSRepository)
        {
            _cMSRepository = cMSRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
                return View("Shop", await _cMSRepository.Product());
        }
    }
}
