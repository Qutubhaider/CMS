using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CMSUtility.Utilities.CommonConstant;

namespace DmfWeb.DeskOP.Stores.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("DeskOP")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Areas/DeskOP/Views/Dashboard/Dashboard.cshtml");
        }
    }
}
