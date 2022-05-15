using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CMSUtility.Utilities.CommonConstant;

namespace DmfWeb.Areas.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Admin")]
    
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Dashboard/Dashboard.cshtml");
        }
    }
}
