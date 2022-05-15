using CMSBAL.Case.Models;
using CMSBAL.FIle.Models;
using CMSBAL.IssueFIleHistory.Models;
using CMSBAL.Repository.IRepository;
using CMSBAL.User.Models;
using CMSUtility.Models;
using CMSUtility.Service.PaginationService;
using CMSUtility.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using static CMSUtility.Utilities.CommonConstant;

namespace FileSystemWeb.Areas.DeskOP.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("DeskOP")]
    public class CaseController : Controller
    {

            private readonly IUnitOfWork moUnitOfWork;
            private readonly static int miPageSize = 10;
            private readonly IWebHostEnvironment moWebHostEnvironment;

        public CaseController(IUnitOfWork foUnitOfWork,IWebHostEnvironment foWebHostEnvironment)
            {
                moUnitOfWork = foUnitOfWork;
            moWebHostEnvironment = foWebHostEnvironment;
            }
            public IActionResult Index()
            {
                return View("~/Areas/DeskOP/Views/Case/CaseList.cshtml");
            }

        public IActionResult GetCaseList(string fsFileName, int Status, int? sort_column, string sort_order, int? pg, int? size)
        {
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

                List<CaseListResult> loCaseListResult = new List<CaseListResult>();
                loCaseListResult = moUnitOfWork.CaseRepository.GetCaseList(fsFileName == null ? fsFileName : fsFileName.Trim(), Status, sort_column, sort_order, pg.Value, size.Value, Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()));
                dynamic loModel = new ExpandoObject();
                loModel.GetCaseList = loCaseListResult;
                if (loCaseListResult.Count > 0)
                {
                    liTotalRecords = loCaseListResult[0].inRecordCount;
                    liStartIndex = loCaseListResult[0].inRownumber;
                    liEndIndex = loCaseListResult[loCaseListResult.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/DeskOP/Views/Case/_CaseList.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }
        }
        public IActionResult Detail(Guid Id)
        {
            CaseDetailResult loCaseDetail = new CaseDetailResult();
            if (Id != Guid.Empty)
            {
                loCaseDetail = moUnitOfWork.CaseRepository.GetCaseDetail(Id);
            }
            //loCaseDetail.UserList = moUnitOfWork.UserRepository.GetUserListByDepartmentId(Convert.ToInt32(User.FindFirst(SessionConstant.DepartmentId).Value));
            loCaseDetail.IssueFileListResult= moUnitOfWork.IssueFileHistoryRepository.GetFileHistoryList(loCaseDetail.inSRId);
            loCaseDetail.DepartmentList = moUnitOfWork.DepartmentRepository.GetDepartmentDropDown();
            return View("~/Areas/DeskOP/Views/Case/CaseDetail.cshtml", loCaseDetail);
        }

        [HttpPost]
        public IActionResult ForwardFile(CaseDetailResult foCaseDetail)
        {
            try
            {
                IssueFile loIssueFile = new IssueFile
                {
                    inAssignUserId = foCaseDetail.assignedTo,
                    inDivisionId = Convert.ToInt32(User.FindFirst(SessionConstant.DivisionId).Value),
                    inDepartmentId = Convert.ToInt32(User.FindFirst(SessionConstant.DepartmentId).Value),
                    inStoreFileDetailsId = foCaseDetail.inStoreFileDetailId,
                    stComment = foCaseDetail.stComment,
                    inStatus = (int)CommonFunctions.FileStatus.Pending,
                    inSRId = foCaseDetail.inSRId,
                    inCaseId = foCaseDetail.inCaseId
                };

                string stUnFileName = string.Empty;
                //string stFile = string.Empty;
                if (foCaseDetail != null)
                {
                    if (foCaseDetail.File != null)
                    {
                        string loFolderPath = Path.Combine(moWebHostEnvironment.WebRootPath, "Files");
                        loIssueFile.stUnFileName = Guid.NewGuid().ToString() + Path.GetExtension(foCaseDetail.File.FileName);
                        loIssueFile.stFileName = foCaseDetail.File.FileName;
                        string filePath = Path.Combine(loFolderPath, loIssueFile.stUnFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            foCaseDetail.File.CopyTo(fileStream);
                        }
                    }
                }


                 int liSuccess = 0;
                int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString());
                moUnitOfWork.IssueFileHistoryRepository.SaveIssueFile(loIssueFile, liUserId, out liSuccess);
                if (liSuccess == (int)CommonFunctions.ActionResponse.Add)
                {
                    TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                    TempData["Message"] = string.Format(AlertMessage.RecordAdded, "Issue File");
                    return RedirectToAction("Index");
                }
                else if (liSuccess == (int)CommonFunctions.ActionResponse.Update)
                {
                    TempData["ResultCode"] = CommonFunctions.ActionResponse.Update;
                    TempData["Message"] = string.Format(AlertMessage.RecordUpdated, "Issue File");
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                    TempData["Message"] = string.Format(AlertMessage.OperationalError, "forwarding file");
                    return RedirectToAction("Index");
                }

            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }
        }
        public IActionResult GetUserDetailFromDropDown(int userId)
        {
            UserDropDownDetailResult user = moUnitOfWork.UserRepository.GetUserDetailFromDropDown(userId);
            return Json(new { data = user });
        }

        public IActionResult GetFileDetailFromDropDown(int fileId)
        {
            StoreFileDetailDropDownResult file = moUnitOfWork.FileRepository.GetFileDetailDropDown(fileId);
            return Json(new { data = file });
        }

        public IActionResult GetUserDropdown(int DepartmentId)
        {
            List<Select2> UserDropDown = moUnitOfWork.UserRepository.GetUserListByDepartmentId(DepartmentId);
            return Json(new { data = UserDropDown });

        }
    }
}
