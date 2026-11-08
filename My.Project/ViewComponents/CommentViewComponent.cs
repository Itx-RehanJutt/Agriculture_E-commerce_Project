using Microsoft.AspNetCore.Mvc;

namespace My.Project.ViewComponents
{
    public class CommentViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Comment");
        }
    }
}
