using CMSBAL.Almirah.Models;
using CMSBAL.Repository.IRepository;
using CMSUtility.Models;
using CMSUtility.Service.PaginationService;
using CMSUtility.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Text;
using static CMSUtility.Utilities.CommonConstant;

namespace FileSystemWeb.Areas.Admin.Controllers
{//
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Admin")]
    public class AlmirahController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;

        public AlmirahController(IUnitOfWork foUnitOfWork)
        {
            moUnitOfWork = foUnitOfWork;
        }

        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Almirah/AlmirahList.cshtml");
        }

        public IActionResult Detail(Guid id)
        {
            Almirah loAlmirah = new Almirah();
            if (id != Guid.Empty)
            {
                loAlmirah = moUnitOfWork.AlmirahRepository.GetAlmirahDetail(id);
            }
            loAlmirah.ZoneList = moUnitOfWork.ZoneRepository.GetZoneDropDown();
            loAlmirah.DepartmentList = moUnitOfWork.DepartmentRepository.GetDepartmentDropDown();
            //loAlmirah.StoreList = moUnitOfWork.StoreRepository.GetStoreDropDown();
            //loAlmirah.RoomList = moUnitOfWork.RoomRepository.GetRoomDropDown();
            return View("~/Areas/Admin/Views/Almirah/AlmirahDetail.cshtml",loAlmirah);
        }
        public IActionResult SaveAlmirah(Almirah foAlmirah)
        {
            try
            {
                int liSuccess = 0;
                int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()); //User.FindFirst(SessionConstant)
                if (foAlmirah != null)
                {
                    moUnitOfWork.AlmirahRepository.SaveAlmirah(foAlmirah, liUserId, out liSuccess);
                    if (liSuccess == (int)CommonFunctions.ActionResponse.Add)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                        TempData["Message"] = string.Format(AlertMessage.RecordAdded, "Almirah");
                        return RedirectToAction("Index");
                    }
                    else if (liSuccess == (int)CommonFunctions.ActionResponse.Update)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Update;
                        TempData["Message"] = string.Format(AlertMessage.RecordUpdated, "Almirah");
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                        TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving almirah");
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving almirah");
                return RedirectToAction("Index");
            }
        }
        public IActionResult GetAlmirahList(string AlmirahNumber, int? Status, int? sort_column, string sort_order, int? pg, int? size)
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

                List<AlmirahListResult> loAlmirahListResults = new List<AlmirahListResult>();
                loAlmirahListResults = moUnitOfWork.AlmirahRepository.GetAlmirahList(AlmirahNumber == null ? AlmirahNumber : AlmirahNumber.Trim(), sort_column, sort_order, pg.Value, size.Value);
                dynamic loModel = new ExpandoObject();
                loModel.GetAlmirahList = loAlmirahListResults;
                if (loAlmirahListResults.Count > 0)
                {
                    liTotalRecords = loAlmirahListResults[0].inRecordCount;
                    liStartIndex = loAlmirahListResults[0].inRownumber;
                    liEndIndex = loAlmirahListResults[loAlmirahListResults.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/Admin/Views/Almirah/_AlmirahListData.cshtml", loModel);
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

    }
}
