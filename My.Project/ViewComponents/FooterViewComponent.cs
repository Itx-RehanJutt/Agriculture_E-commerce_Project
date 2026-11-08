using Microsoft.AspNetCore.Mvc;

namespace My.Project.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Footer");
        }
    }
}
