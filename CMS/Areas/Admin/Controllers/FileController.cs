using CMSBAL.FIle.Models;
using CMSBAL.Repository.IRepository;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMSUtility.Utilities.CommonConstant;

namespace FileSystemWeb.Areas.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Admin")]
    public class FileController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;
        private readonly IWebHostEnvironment moWebHostEnvironment;

        public FileController(IUnitOfWork foUnitOfWork, IWebHostEnvironment foWebHostEnvironment)
        {
            moUnitOfWork = foUnitOfWork;
            moWebHostEnvironment = foWebHostEnvironment;
        }
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/File/FileList.cshtml");
        }
        public IActionResult Detail(Guid id)
        {
            FileDetail loFileDetail = new FileDetail();
            if (id != Guid.Empty)
            {
                loFileDetail =  moUnitOfWork.FileRepository.GetFileDetail(id);
            }
            loFileDetail.ZoneList = moUnitOfWork.ZoneRepository.GetZoneDropDown();
            loFileDetail.DepartmentList = moUnitOfWork.DepartmentRepository.GetDepartmentDropDown();
            //loFileDetail.StoreList = moUnitOfWork.StoreRepository.GetStoreDropDown();
            //sloState.StateList = moUnitOfWork.StateRepository.GetStateDropDown();
            return View("~/Areas/Admin/Views/File/FileDetail.cshtml", loFileDetail);
        }

        public IActionResult SaveFile(FileDetail foFileDetail)
        {
                try
                {
                    int liSuccess = 0;
                    int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()); //User.FindFirst(SessionConstant)
                    if (foFileDetail != null)
                    {
                    if (foFileDetail.File != null)
                    {
                        string loFolderPath = Path.Combine(moWebHostEnvironment.WebRootPath, "Files");
                        foFileDetail.stUnFileName = Guid.NewGuid().ToString() + Path.GetExtension(foFileDetail.File.FileName);
                        foFileDetail.stFileName = foFileDetail.File.FileName;
                        string filePath = Path.Combine(loFolderPath, foFileDetail.stUnFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            foFileDetail.File.CopyTo(fileStream);
                        }
                    }
                    moUnitOfWork.FileRepository.SaveFile(foFileDetail, liUserId, out liSuccess);
                        if (liSuccess == (int)CommonFunctions.ActionResponse.Add)
                        {
                            TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                            TempData["Message"] = string.Format(AlertMessage.RecordAdded, "Shelve");
                            return RedirectToAction("Index");
                        }
                        else if (liSuccess == (int)CommonFunctions.ActionResponse.Update)
                        {
                            TempData["ResultCode"] = CommonFunctions.ActionResponse.Update;
                            TempData["Message"] = string.Format(AlertMessage.RecordUpdated, "Shelve");
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                            TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving shelve");
                            return RedirectToAction("Index");
                        }

                    }
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                    TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving shelve");
                    return RedirectToAction("Index");
                }
        }
        public IActionResult GetFileList(string fsFileName, int? Status, int? sort_column, string sort_order, int? pg, int? size)
        {
            StringBuilder lolog = new StringBuilder();
            try
            {
                /*lolog.AppendLine("DeskName : " + DeskName);
                lolog.AppendLine("Status : " + Status);
                lolog.AppendLine("Sort Column : " + sort_column);
                lolog.AppendLine("Sort Order : " + sort_order);
                lolog.AppendLine("Page No : " + pg);
                lolog.AppendLine("Page Size : " + size);
*/
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

                List<FileListResult> loFileListResults = new List<FileListResult>();
                loFileListResults = moUnitOfWork.FileRepository.GetFileList(fsFileName == null ? fsFileName : fsFileName.Trim(), sort_column, sort_order, pg.Value, size.Value);
                dynamic loModel = new ExpandoObject();
                loModel.GetFileList = loFileListResults;
                if (loFileListResults.Count > 0)
                {
                    liTotalRecords = loFileListResults[0].inRecordCount;
                    liStartIndex = loFileListResults[0].inRownumber;
                    liEndIndex = loFileListResults[loFileListResults.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/Admin/Views/File/_FileListData.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }

        }
        public IActionResult GetDivisionDropDown(int fiZoneId)
        {
            List<Select2> DivisionDropDown = moUnitOfWork.DivisionRepository.GetDivisionDropDown(fiZoneId);
            return Json(new { data = DivisionDropDown });


        }
        public IActionResult GetRoomDropDown(int fiStoreId)
        {
            List<Select2> RoomDropDown = moUnitOfWork.RoomRepository.GetRoomDropDown(fiStoreId);
            return Json(new { data = RoomDropDown });

        }
        public IActionResult GetStoreDropDown(int fiDivisionId)
        {
            List<Select2> StoreDropDown = moUnitOfWork.StoreRepository.GetStoreDropDown(fiDivisionId);
            return Json(new { data = StoreDropDown });

        }
        public IActionResult GetAlmirahDropDown(int fiRoomId)
        {
            List<Select2> AlmirahDropDown = moUnitOfWork.AlmirahRepository.GetAlmirahDropDown(fiRoomId);
            return Json(new { data = AlmirahDropDown });

        }
        public IActionResult GetShelvesDropDown(int fiAlmirahId)
        {
            List<Select2> ShelveDropDown = moUnitOfWork.ShelveRepository.GetShelveDropDown(fiAlmirahId);
            return Json(new { data = ShelveDropDown });
        }

        public IActionResult DownloadFile(string fuFileName,string fileName)
        {
            return File(System.IO.File.ReadAllBytes(Path.Combine(moWebHostEnvironment.WebRootPath, "Files", fuFileName)), "application/octet-stream", fileName);
        }

    }
}
