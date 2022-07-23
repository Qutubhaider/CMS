using CMSBAL.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMSBAL.Category.Models;
using System.Collections.Generic;
using System;
using static CMSUtility.Utilities.CommonConstant;
using CMSUtility.Utilities;
using System.Text;
using System.Dynamic;
using CMSUtility.Service.PaginationService;
using CMSUtility.Models;

namespace FileSystemWeb.Areas.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;
        public CategoryController(IUnitOfWork foUnitOfWork)
        {
            moUnitOfWork = foUnitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCategoryList(string categoryName, int? Status, int? sort_column, string sort_order, int? pg, int? size)
        {
            StringBuilder lolog = new StringBuilder();
            try
            {
                lolog.AppendLine("categoryName : " + categoryName);
                lolog.AppendLine("Status : " + Status);
                lolog.AppendLine("Sort Column : " + sort_column);
                lolog.AppendLine("Sort Order : " + sort_order);
                lolog.AppendLine("Page No : " + pg);
                lolog.AppendLine("Page Size : " + size);

                string lsSearch = string.Empty;
                int liTotalRecords = 0, liStartIndex = 0, liEndIndex = 0;
                if (sort_column == 0 || sort_column == null)
                    sort_column = 1;
                if (string.IsNullOrEmpty(sort_order) || sort_order == "desc")
                {
                    sort_order = "desc";
                    ViewData["sortorder"] = "asc";
                }
                else
                {
                    ViewData["sortorder"] = "desc";
                }
                if (pg == null || pg <= 0)
                    pg = 1;
                if (size == null || size.Value <= 0)
                    size = miPageSize;

                List<CategoryListResult> loCategoryList = new List<CategoryListResult>();
                loCategoryList = moUnitOfWork.CategoryRepository.GetCategoriesList(categoryName == null ? categoryName : categoryName.Trim(), Status, sort_column, sort_order, pg.Value, size.Value);
                dynamic loModel = new ExpandoObject();
                loModel.GetCategoryList = loCategoryList;
                if (loCategoryList.Count > 0)
                {
                    liTotalRecords = loCategoryList[0].inRecordCount;
                    liStartIndex = loCategoryList[0].inRownumber;
                    liEndIndex = loCategoryList[loCategoryList.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/Admin/Views/Category/_CategoryList.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }

        }

        public IActionResult Detail(Guid id)
        {
          CMSBAL.Category.Models.Category loCategory = new CMSBAL.Category.Models.Category();
            if ( id != Guid.Empty)
            {
                loCategory = moUnitOfWork.CategoryRepository.GetCategory(id);
            }
            loCategory.DepartmentList = moUnitOfWork.DepartmentRepository.GetDepartmentDropDown();
            //loCategory.CategoryList = moUnitOfWork.CategoryRepository.GetCategoryDropDown();
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

        [HttpGet]
        public IActionResult GetParentCategoryDropdown(int fiDepartmentId)
        {
            List<Select2> ParentCategoryDropDown = moUnitOfWork.CategoryRepository.GetCategory(fiDepartmentId);
            return Json(new { data = ParentCategoryDropDown });
        }
    }
}
