using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CMSUtility.Utilities.CommonConstant;

namespace DmfWeb.Areas.Divisions.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Divisions")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Areas/Divisions/Views/Dashboard/Dashboard.cshtml");
        }
    }
}
