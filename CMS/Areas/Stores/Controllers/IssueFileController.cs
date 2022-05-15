using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using static CMSUtility.Utilities.CommonConstant;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileSystemWeb.Areas.Stores.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Stores")]
    public class IssueFileController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;
        private readonly IWebHostEnvironment moWebHostEnvironment;
        public IssueFileController(IUnitOfWork foUnitOfWork, IWebHostEnvironment foWebHostEnvironment)
        {
            moUnitOfWork = foUnitOfWork;
            moWebHostEnvironment = foWebHostEnvironment;
        }
        public IActionResult Index()
        {
            return View("~/Areas/Stores/Views/IssueFile/IssueFileList.cshtml");
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
            return View("~/Areas/Stores/Views/IssueFile/IssueFile.cshtml", loIssueFile);
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
                loIssueFileListResult = moUnitOfWork.IssueFileHistoryRepository.GetIssueFileListByStore(fsFileName == null ? fsFileName : fsFileName.Trim(), sort_column, sort_order, pg.Value, size.Value, Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()));
                dynamic loModel = new ExpandoObject();
                loModel.GetIssueFileList = loIssueFileListResult;
                if (loIssueFileListResult.Count > 0)
                {
                    liTotalRecords = loIssueFileListResult[0].inRecordCount;
                    liStartIndex = loIssueFileListResult[0].inRownumber;
                    liEndIndex = loIssueFileListResult[loIssueFileListResult.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/Stores/Views/IssueFile/_IssueFileListData.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }
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

        public IActionResult DownloadFile(string fuFileName, string fileName)
        {
            return File(System.IO.File.ReadAllBytes(Path.Combine(moWebHostEnvironment.WebRootPath, "Files", fuFileName)), "application/octet-stream", fileName);
        }

        public IActionResult GetUserDetailFromDropDown(int userId)
        {
            UserDropDownDetailResult user = moUnitOfWork.UserRepository.GetUserDetailFromDropDown(userId);

            return Json(new { data = user});
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

