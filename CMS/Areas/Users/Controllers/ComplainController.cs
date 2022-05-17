using CMSBAL.Complain.Models;
using CMSBAL.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using static CMSUtility.Utilities.CommonConstant;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Areas.Users.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Users")]
    public class ComplainController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;

        public ComplainController(IUnitOfWork foUnitOfWork)
        {
            moUnitOfWork = foUnitOfWork;
        }
        public IActionResult Index()
        {
            return View("~/Areas/Users/Views/Complain/ComplainList.cshtml");
        }
        public IActionResult AddComplain()
        {
            Complain loComplain = new Complain();
            loComplain.DepartmentList = moUnitOfWork.DepartmentRepository.GetDepartmentDropDown();
            return View("~/Areas/Users/Views/Complain/AddComplain.cshtml", loComplain);
        }
        [HttpPost]
        public IActionResult SaveComplain(Complain foComplain)
        {
            int liSuccess = 0;
            int liZoneId = Convert.ToInt32(User.FindFirst(SessionConstant.ZoneId).Value.ToString());
            int liDivisionId = Convert.ToInt32(User.FindFirst(SessionConstant.DivisionId).Value.ToString());
            int liUser = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString());
            return View();
        }
    }
}
