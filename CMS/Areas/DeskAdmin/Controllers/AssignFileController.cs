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

namespace FileSystemWeb.Areas.DeskAdmin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("DeskAdmin")]
    public class AssignFileController : Controller
    {

        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;
        private readonly IWebHostEnvironment moWebHostEnvironment;

        public AssignFileController(IUnitOfWork foUnitOfWork, IWebHostEnvironment foWebHostEnvironment)
        {
            moUnitOfWork = foUnitOfWork;
            moWebHostEnvironment = foWebHostEnvironment;
        }
        public IActionResult Index()
        {
            return View("~/Areas/DeskAdmin/Views/AssignFile/AssignFileList.cshtml");
        }

      
        public IActionResult GetIssueFileList(string fsFileName, int? Status, int? sort_column, string sort_order, int? pg, int? size)
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

                List<IssueFileListResult> loIssueFileListResult = new List<IssueFileListResult>();
                loIssueFileListResult = moUnitOfWork.IssueFileHistoryRepository.GetIssueFileListByDeskAdmin(fsFileName == null ? fsFileName : fsFileName.Trim(), sort_column, sort_order, pg.Value, size.Value, Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()));
                dynamic loModel = new ExpandoObject();
                loModel.GetIssueFileList = loIssueFileListResult;
                if (loIssueFileListResult.Count > 0)
                {
                    liTotalRecords = loIssueFileListResult[0].inRecordCount;
                    liStartIndex = loIssueFileListResult[0].inRownumber;
                    liEndIndex = loIssueFileListResult[loIssueFileListResult.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/DeskAdmin/Views/AssignFile/_AssignFileList.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }
        }

      

        public IActionResult AssignFileDetail(Guid? id)
        {
            GetAssignFileDetailResult assignedFile = moUnitOfWork.IssueFileHistoryRepository.AssignFileDetailResult(id);

            return View("~/Areas/DeskAdmin/Views/AssignFile/AssignFileDetail.cshtml", assignedFile);
        }

        [HttpPost]

        public IActionResult AcceptAssignFile(GetAssignFileDetailResult loAssignFile)
        {
           
            try
            {
                Case locase = new Case();

                locase.inZoneId = Convert.ToInt32(User.FindFirst(SessionConstant.ZoneId).Value);
                locase.inDepartmentId = Convert.ToInt32(User.FindFirst(SessionConstant.DepartmentId).Value);
                locase.inDivisionId = Convert.ToInt32(User.FindFirst(SessionConstant.DivisionId).Value);
                locase.inDesignationId = Convert.ToInt32(User.FindFirst(SessionConstant.DesignationId).Value);
                locase.inStoreFileDetailId = loAssignFile.inStoreFileDetailsId;
                locase.stComment = loAssignFile.stComment;
                locase.inAcceptedBy = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value);
                locase.inIssueFileId = loAssignFile.inlssueFileId;
                int liSuccess = 0;
                int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()); //User.FindFirst(SessionConstant)
                if (locase != null)
                {
                    moUnitOfWork.CaseRepository.SaveCase(locase, liUserId, out liSuccess);
                    if (liSuccess == (int)CommonFunctions.ActionResponse.Add)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                        TempData["Message"] = string.Format(AlertMessage.RecordAdded, "Case");
                        return RedirectToAction("Index");
                    }
                    else if (liSuccess == (int)CommonFunctions.ActionResponse.Update)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Update;
                        TempData["Message"] = string.Format(AlertMessage.RecordUpdated, "Case");
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                        TempData["Message"] = string.Format(AlertMessage.OperationalError, "accepting case");
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                TempData["Message"] = string.Format(AlertMessage.OperationalError, "accepting case");
                return RedirectToAction("Index");
            }
            return null;           

        }
        public IActionResult DownloadFile(string fuFileName, string fileName)
        {
            return File(System.IO.File.ReadAllBytes(Path.Combine(moWebHostEnvironment.WebRootPath, "Files", fuFileName)), "application/octet-stream", fileName);
        }

        public IActionResult GetIssueFileList1(int SRId, int? sort_column, string sort_order, int? pg, int? size)
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

                List<IssueFileListResult> loIssueFileListResult = new List<IssueFileListResult>();
                loIssueFileListResult = moUnitOfWork.IssueFileHistoryRepository.GetIssueFileListBySR(SRId, sort_column, sort_order, pg.Value, size.Value);
                dynamic loModel = new ExpandoObject();
                loModel.GetIssueFileList = loIssueFileListResult;
                if (loIssueFileListResult.Count > 0)
                {
                    liTotalRecords = loIssueFileListResult[0].inRecordCount;
                    liStartIndex = loIssueFileListResult[0].inRownumber;
                    liEndIndex = loIssueFileListResult[loIssueFileListResult.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/DeskAdmin/Views/AssignFile/_IssueFiles.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }
        }

        public IActionResult Detail(Guid id)
        {
            IssueFile loIssueFile = new IssueFile();
            if (id != Guid.Empty)
            {
                loIssueFile = moUnitOfWork.IssueFileHistoryRepository.GetIssueFileDetail(id);
            }

            loIssueFile.inDivisionId = Convert.ToInt32(User.FindFirst(SessionConstant.DivisionId).Value.ToString());
            loIssueFile.inDepartmentId = Convert.ToInt32(User.FindFirst(SessionConstant.DepartmentId).Value.ToString());
            loIssueFile.RoomList = moUnitOfWork.RoomRepository.GetRoomDropDown(Convert.ToInt32(User.FindFirst(SessionConstant.StoreId).Value.ToString()));
            loIssueFile.DepartmentList = moUnitOfWork.DepartmentRepository.GetDepartmentDropDown();
            loIssueFile.UserList = moUnitOfWork.UserRepository.GetUserListForIssueFile(Convert.ToInt32(User.FindFirst(SessionConstant.StoreId).Value.ToString()), Convert.ToInt32(User.FindFirst(SessionConstant.DivisionId).Value.ToString()));
            loIssueFile.FileList = moUnitOfWork.FileRepository.GetFileDropDown();
            return View("~/Areas/DeskAdmin/Views/AssignFile/AssignFileDetail.cshtml", loIssueFile);
        }

       
        public IActionResult SaveIssueFile(IssueFile foIssueFileDetail)
        {
            try
            {
                int liSuccess = 0;
                int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString());
                if (foIssueFileDetail != null)
                {

                    moUnitOfWork.IssueFileHistoryRepository.SaveIssueFileByStore(foIssueFileDetail, liUserId, out liSuccess);
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
                        TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving issue file");
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving issue file");
                return RedirectToAction("Index");
            }
        }

        /*public IActionResult DownloadFile(string fuFileName, string fileName)
        {
            return File(System.IO.File.ReadAllBytes(Path.Combine(moWebHostEnvironment.WebRootPath, "Files", fuFileName)), "application/octet-stream", fileName);
        }*/

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
            List<Select2> UserDropDown = moUnitOfWork.UserRepository.GetUserListByDepartmentId(DepartmentId, Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()));
            return Json(new { data = UserDropDown });

        }
    }
}
