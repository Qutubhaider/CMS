using CMSBAL.Repository.IRepository;
using CMSBAL.Zone.Models;
using CMSUtility.Service.PaginationService;
using CMSUtility.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMSUtility.Utilities.CommonConstant;

namespace FileSystemWeb.Areas.Admin.Controllers
{

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Admin")]
    public class ZoneController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;
        public ZoneController(IUnitOfWork foUnitOfWork)
        {
            moUnitOfWork = foUnitOfWork;
        }
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Zone/ZoneList.cshtml");
        }
        public IActionResult Detail(Guid id)
        {
            Zone loZone = new Zone();
            if (id != Guid.Empty)
            {
                loZone = moUnitOfWork.ZoneRepository.GetZoneDetail(id);
            }
            //sloState.StateList = moUnitOfWork.StateRepository.GetStateDropDown();
            return View("~/Areas/Admin/Views/Zone/ZoneDetail.cshtml", loZone);
        }
        public IActionResult SaveZone(Zone foZone)
        {
            try
            {
                int liSuccess = 0;
                int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()); //User.FindFirst(SessionConstant)
                if (foZone != null)
                {
                    moUnitOfWork.ZoneRepository.SaveZone(foZone, liUserId, out liSuccess);
                    if (liSuccess == (int)CommonFunctions.ActionResponse.Add)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                        TempData["Message"] = string.Format(AlertMessage.RecordAdded, "Zone");
                        return RedirectToAction("Index");
                    }
                    else if (liSuccess == (int)CommonFunctions.ActionResponse.Update)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Update;
                        TempData["Message"] = string.Format(AlertMessage.RecordUpdated, "Zone");
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                        TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving zone");
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving zone");
                return RedirectToAction("Index");
            }
        }
        public IActionResult GetZonetList(string ZoneName, int? Status, int? sort_column, string sort_order, int? pg, int? size)
        {
            StringBuilder lolog = new StringBuilder();
            try
            {
                lolog.AppendLine("ProjectTitle : " + ZoneName);
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

                List<ZoneListResult> loZoneListResults = new List<ZoneListResult>();
                loZoneListResults = moUnitOfWork.ZoneRepository.GetZoneList(ZoneName == null ? ZoneName : ZoneName.Trim(), Status, sort_column, sort_order, pg.Value, size.Value);
                dynamic loModel = new ExpandoObject();
                loModel.GetZoneList = loZoneListResults;
                if (loZoneListResults.Count > 0)
                {
                    liTotalRecords = loZoneListResults[0].inRecordCount;
                    liStartIndex = loZoneListResults[0].inRownumber;
                    liEndIndex = loZoneListResults[loZoneListResults.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/Admin/Views/Zone/_ZoneListData.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }

        }
    }
}
