using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CMSUtility.Utilities.CommonConstant;

namespace FileSystemWeb.Areas.Users.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Users")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Areas/Users/Views/Dashboard/Dashboard.cshtml");
        }
    }
}
