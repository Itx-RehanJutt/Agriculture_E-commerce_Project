using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace My.Project.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        private int theUser;
        public int TheUser
        {
            get { return theUser; }
        }
        private int userRoleId;
        public int UserRoleId
        {
            get { return userRoleId; }
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Session.GetInt32("UserId").HasValue)
            {
                context.Result = new RedirectToRouteResult(
                        new { area = "Admin", controller = "Home", action = "Index" });
            }
            else
            {

                theUser = context.HttpContext.Session.GetInt32("UserId").Value;
                userRoleId = context.HttpContext.Session.GetInt32("UserRoleId").Value;
                base.OnActionExecuting(context);
            }
        }
    }
}
