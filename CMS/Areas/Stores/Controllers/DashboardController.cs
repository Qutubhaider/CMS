using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CMSUtility.Utilities.CommonConstant;

namespace DmfWeb.Areas.Stores.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Stores")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Areas/Stores/Views/Dashboard/Dashboard.cshtml");
        }
    }
}
