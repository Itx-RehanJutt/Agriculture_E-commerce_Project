using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My.Project.Models;
using My.Project.Utilities;
using System.Diagnostics;
using System.Security.Claims;

namespace My.Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthRepository _iAuthRepository;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger , IAuthRepository iAuthRepository)
        {
            _iAuthRepository = iAuthRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Auth()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Auth(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                ModelState.AddModelError("", "Please fill out the form Correctly.");
                return View(model);
            }
            var (user , ErrorMessage) = await _iAuthRepository.Auth(model);
            if (user == null)
            {
                ModelState.AddModelError("", ErrorMessage);
                return View(model);
            }
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, "User") // Or based on user role
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var authProperties = new AuthenticationProperties
            //{
            //    IsPersistent = model.RememberMe
            //};

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            HttpContext.Session.SetObject("User", user);
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetInt32("UserRoleId", (int)user.RoleId);
            HttpContext.Session.SetInt32("CustomerId", (int)user.CustomerId);
            return RedirectToAction("Index");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                ModelState.AddModelError("", "Please fill out the form Correctly.");
                return View(model);
            }
            var (user , ErrorMessage) = await _iAuthRepository.SignUp(model);
            if (user == null)
            {
                ModelState.AddModelError("", ErrorMessage);
                return View(model);
            }
            ModelState.AddModelError("", ErrorMessage);
            return RedirectToAction("Auth");
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Service()
        {
            return View();
        }
        public IActionResult Shop()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpGet]
        [Route("Home/ProductDetail/{id}")]
        public IActionResult ProductDetail(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID Not Found");
            }
            else
            {
                ViewBag.Id = id;
            }
            return View();
        }

        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult NotAuthorized()
        {
            return View();
        }
        public IActionResult signout()
        {
            HttpContext.Session.Remove("User");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserRoleId");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
