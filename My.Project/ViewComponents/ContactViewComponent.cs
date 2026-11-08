using Microsoft.AspNetCore.Mvc;

namespace My.Project.ViewComponents
{
    public class ContactViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Contact");
        }
    }
}
