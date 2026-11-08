using Microsoft.AspNetCore.Mvc;

namespace My.Project.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        public NavbarViewComponent() 
        {

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Navbar");
        }
    }
}
