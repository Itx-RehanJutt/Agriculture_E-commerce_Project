using Microsoft.AspNetCore.Mvc;

namespace My.Project.ViewComponents
{
    public class ListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("List");
        }
    }
}
