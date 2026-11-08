using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using My.Project.Utilities;

namespace My.Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IAuthRepository _authRepository;

        public HomeController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var (user , ErrorMessage) = await _authRepository.Signin(model);

                if (user == null)
                {
                    ModelState.AddModelError("", ErrorMessage);
                    return View(model);
                }
                    // Login Success
                    HttpContext.Session.SetObject("User", user);
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    HttpContext.Session.SetInt32("UserRoleId", (int)user.RoleId);
                    return RedirectToAction("Index", new { Controller = "Onbording", area = "Admin" });
                
            }
            return View();
        }
        public IActionResult signout()
        {
            HttpContext.Session.Remove("User");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserRoleId");
            return RedirectToAction("Index");
        }
    }
}
