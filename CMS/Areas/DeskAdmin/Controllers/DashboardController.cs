using CMSBAL.Dashboard.Models;
using CMSBAL.Repository.IRepository;
using CMSUtility.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using static CMSUtility.Utilities.CommonConstant;

namespace DmfWeb.DeskAdmin.Stores.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("DeskAdmin")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;
        public DashboardController(IUnitOfWork foUnitOfWork)
        {
            moUnitOfWork = foUnitOfWork;
        }
        public IActionResult Index()
        {
            try
            {
                List<DashboardResult> loDashboardResult = new List<DashboardResult>();
                loDashboardResult = moUnitOfWork.DashboardRepository.GetDepartmentDashboard(Convert.ToInt32(User.FindFirst(SessionConstant.DesignationId).Value.ToString()));
                dynamic loModel = new ExpandoObject();
                loModel.GetDashboardResult = loDashboardResult;
                return PartialView("~/Areas/DeskAdmin/Views/Dashboard/Dashboard.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }
        }
    }
}
