using Microsoft.AspNetCore.Mvc;

namespace My.Project.ViewComponents
{
    public class CartListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("CartList");
        }
    }
}
