using CMSBAL.Dashboard.Models;
using CMSBAL.Repository.IRepository;
using CMSUtility.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            /* DashboardResult loDashboardResult = new DashboardResult();
             moUnitOfWork.DashboardRepository.getStoreUserCount(Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value),(int)CommonConstant.UserType.DeskAdmin,out int inStoreUserCount);
             loDashboardResult.inStoreUserCount= inStoreUserCount;
             moUnitOfWork.DashboardRepository.getDeskOperatorCount(Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value), (int)CommonConstant.UserType.DeskAdmin, out int inDeskOpCount);
             loDashboardResult.inDeskOperatorCount = inDeskOpCount;
             moUnitOfWork.DashboardRepository.getPendingAcceptFileCount(Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value), (int)CommonConstant.UserType.DeskAdmin, out int inPendingAcceptFileCount);
             loDashboardResult.inPendingAcceptFileCount = inPendingAcceptFileCount;
             moUnitOfWork.DashboardRepository.getPendingCaseCount(Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value), (int)CommonConstant.UserType.DeskAdmin, out int inCaseCount);
             loDashboardResult.inPendingCaseCount = inCaseCount;*/
            return View("~/Areas/DeskAdmin/Views/Dashboard/Dashboard.cshtml");
            //return View("~/Areas/DeskAdmin/Views/Dashboard/Dashboard.cshtml",loDashboardResult);
        }
    }
}
