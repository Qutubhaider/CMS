using CMSBAL.Repository.IRepository;
using CMSBAL.Shelve.Models;
using CMSUtility.Models;
using CMSUtility.Service.PaginationService;
using CMSUtility.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using static CMSUtility.Utilities.CommonConstant;

namespace FileSystemWeb.Areas.Stores.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Stores")]
    public class ShelvesController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;

        public ShelvesController(IUnitOfWork foUnitOfWork)
        {
            moUnitOfWork = foUnitOfWork;
        }

        public IActionResult Index()
        {
            return View("~/Areas/Stores/Views/Shelves/ShelvesList.cshtml");
        }

        public IActionResult Detail(Guid id)
        {
            Shelve loShelve = new Shelve();
            if (id != Guid.Empty)
            {
                loShelve = moUnitOfWork.ShelveRepository.GetShelveDetail(id);
            }
            loShelve.inZoneId = Convert.ToInt32(User.FindFirst(SessionConstant.ZoneId).Value.ToString());
            loShelve.inDivisionId = Convert.ToInt32(User.FindFirst(SessionConstant.DivisionId).Value.ToString());
            loShelve.inDepartmentId = Convert.ToInt32(User.FindFirst(SessionConstant.DepartmentId).Value.ToString());
            loShelve.inStoreId = Convert.ToInt32(User.FindFirst(SessionConstant.StoreId).Value.ToString());
            loShelve.RoomList = moUnitOfWork.RoomRepository.GetRoomDropDown(Convert.ToInt32(User.FindFirst(SessionConstant.StoreId).Value.ToString()));
            return View("~/Areas/Stores/Views/Shelves/ShelvesDetail.cshtml", loShelve);
        }
        public IActionResult SaveShelve(Shelve foShelve)
        {
            try
            {
                int liSuccess = 0;
                int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString());
                if (foShelve != null)
                {
                    moUnitOfWork.ShelveRepository.SaveShelve(foShelve, liUserId, out liSuccess);
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
        public IActionResult GetShelveList(string ShelveNumber, int? Status, int? sort_column, string sort_order, int? pg, int? size)
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

                List<ShelveListResult> loShelveListResults = new List<ShelveListResult>();
                loShelveListResults = moUnitOfWork.ShelveRepository.GetShelveList(ShelveNumber == null ? ShelveNumber : ShelveNumber.Trim(), sort_column, sort_order, pg.Value, size.Value, Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()));
                dynamic loModel = new ExpandoObject();
                loModel.GetShelveList = loShelveListResults;
                if (loShelveListResults.Count > 0)
                {
                    liTotalRecords = loShelveListResults[0].inRecordCount;
                    liStartIndex = loShelveListResults[0].inRownumber;
                    liEndIndex = loShelveListResults[loShelveListResults.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/Stores/Views/Shelves/_ShelvesListData.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }

        }
        
        public IActionResult GetAlmirahDropDown(int fiRoomId)
        {
            List<Select2> AlmirahDropDown = moUnitOfWork.AlmirahRepository.GetAlmirahDropDown(fiRoomId);
            return Json(new { data = AlmirahDropDown });

        }
    }
}
