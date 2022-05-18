using CMSBAL.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMSBAL.Category.Models;
using System.Collections.Generic;
using System;
using static CMSUtility.Utilities.CommonConstant;
using CMSUtility.Utilities;

namespace FileSystemWeb.Areas.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        public CategoryController(IUnitOfWork foUnitOfWork)
        {
            moUnitOfWork = foUnitOfWork;
        }
        public IActionResult Index()
        {
            List<CategoryListResult> loCategoryList = moUnitOfWork.CategoryRepository.GetCategoriesList();

            return View(loCategoryList);
        }

        public IActionResult Detail(Guid id)
        {
          CMSBAL.Category.Models.Category loCategory = new CMSBAL.Category.Models.Category();
            if ( id != Guid.Empty)
            {
                loCategory = moUnitOfWork.CategoryRepository.GetCategory(id);
            }
            loCategory.DepartmentList = moUnitOfWork.DepartmentRepository.GetDepartmentDropDown();
            loCategory.CategoryList = moUnitOfWork.CategoryRepository.GetCategoryDropDown();
            return View("~/Areas/Admin/Views/Category/Detail.cshtml", loCategory);
        }

        [HttpPost]
        public IActionResult SaveCategory(CMSBAL.Category.Models.Category foCategory)
        {
            int liSuccess = 0;
            int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()); 
            if (foCategory != null)
            {
                moUnitOfWork.CategoryRepository.SaveCategory(foCategory, liUserId, out liSuccess);
                if (liSuccess == (int)CommonFunctions.ActionResponse.Add)
                {
                    TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                    TempData["Message"] = string.Format(AlertMessage.RecordAdded, "Category");
                    return RedirectToAction("Index");
                }
                else if (liSuccess == (int)CommonFunctions.ActionResponse.Update)
                {
                    TempData["ResultCode"] = CommonFunctions.ActionResponse.Update;
                    TempData["Message"] = string.Format(AlertMessage.RecordUpdated, "Category");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                    TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving category");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
