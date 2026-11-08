using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace My.Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OnbordingController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
