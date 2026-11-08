using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace My.Project.ViewComponents
{
    public class OrderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Order");
        }
    }
}