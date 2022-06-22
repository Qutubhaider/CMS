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
using System.Text;
using System.Dynamic;
using CMSUtility.Service.PaginationService;
using CMSBAL.IssueFIleHistory.Models;

namespace CMS.Areas.DeskAdmin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("DeskAdmin")]
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
            return View("~/Areas/DeskAdmin/Views/Complain/ComplainList.cshtml");
        }

        public IActionResult GetIssueFileList(string fsFileName, int? Status, int? sort_column, string sort_order, int? pg, int? size)
        {
            StringBuilder lolog = new StringBuilder();
            try
            {
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

                List<IssueFileListResult> loIssueFileListResult = new List<IssueFileListResult>();
                loIssueFileListResult = moUnitOfWork.IssueFileHistoryRepository.GetIssueFileListByUser(fsFileName == null ? fsFileName : fsFileName.Trim(), sort_column, sort_order, pg.Value, size.Value,null, Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()));
                dynamic loModel = new ExpandoObject();
                loModel.GetIssueFileList = loIssueFileListResult;
                if (loIssueFileListResult.Count > 0)
                {
                    liTotalRecords = loIssueFileListResult[0].inRecordCount;
                    liStartIndex = loIssueFileListResult[0].inRownumber;
                    liEndIndex = loIssueFileListResult[loIssueFileListResult.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/DeskAdmin/Views/Complain/_ComplainList.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public IActionResult AddComplain()
        {
            Complain loComplain = new Complain();
            loComplain.DepartmentList = moUnitOfWork.DepartmentRepository.GetDepartmentDropDown();
            return View("~/Areas/DeskAdmin/Views/Complain/AddComplain.cshtml", loComplain);
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
