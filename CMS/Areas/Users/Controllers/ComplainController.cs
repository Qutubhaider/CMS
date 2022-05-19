using CMSBAL.Complain.Models;
using CMSBAL.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using static CMSUtility.Utilities.CommonConstant;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using CMSUtility.Models;
using System.Collections.Generic;
using CMSUtility.Utilities;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CMS.Areas.Users.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Users")]
    public class ComplainController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;
        private readonly IWebHostEnvironment moWebHostEnvironment;

        public ComplainController(IUnitOfWork foUnitOfWork, IWebHostEnvironment foWebHostEnvironment)
        {
            moUnitOfWork = foUnitOfWork;
            moWebHostEnvironment = foWebHostEnvironment;
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
            try
            {
                int liSuccess = 0;
                int liZoneId = Convert.ToInt32(User.FindFirst(SessionConstant.ZoneId).Value.ToString());
                int liDivisionId = Convert.ToInt32(User.FindFirst(SessionConstant.DivisionId).Value.ToString());
                int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString());
                int liStoreId = Convert.ToInt32(User.FindFirst(SessionConstant.StoreId).Value.ToString());
                if (foComplain != null)
                {
                    if (foComplain.File != null)
                    {
                        string loFolderPath = Path.Combine(moWebHostEnvironment.WebRootPath, "Files");
                        foComplain.stUnFileName = Guid.NewGuid().ToString() + Path.GetExtension(foComplain.File.FileName);
                        foComplain.stFileName = foComplain.File.FileName;
                        string filePath = Path.Combine(loFolderPath, foComplain.stUnFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            foComplain.File.CopyTo(fileStream);
                        }
                    }
                    moUnitOfWork.ComplainRepository.SaveComplain(foComplain, liZoneId, liDivisionId, liUserId, liStoreId, out liSuccess);
                    if (liSuccess == (int)CommonFunctions.ActionResponse.Add)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                        TempData["Message"] = string.Format(AlertMessage.RecordAdded, "complain");
                        return RedirectToAction("Index");
                    }
                    else if (liSuccess == (int)CommonFunctions.ActionResponse.Update)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Update;
                        TempData["Message"] = string.Format(AlertMessage.RecordUpdated, "complain");
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                        TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving complain");
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving complain");
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public IActionResult GetParentCategoryDropdown(int fiDepartmentId)
        {
            List<Select2> ParentCategoryDropDown = moUnitOfWork.CategoryRepository.GetCategory(fiDepartmentId);
            return Json(new { data = ParentCategoryDropDown });
        }

        [HttpGet]
        public IActionResult GetSubCategoryDropdown(int fiParentCategoryId)
        {
            List<Select2> SubCategoryDropDown = moUnitOfWork.CategoryRepository.GetSubCategoryDropDown(fiParentCategoryId);
            return Json(new { data = SubCategoryDropDown });
        }
    }
}
