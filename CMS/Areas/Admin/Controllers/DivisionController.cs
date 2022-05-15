using CMSBAL.Division.Models;
using CMSBAL.Repository.IRepository;
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

namespace DmfWeb.Areas.Admin.Controllers
{

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Admin")]
    public class DivisionController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;
        public DivisionController(IUnitOfWork foUnitOfWork)
        {
            moUnitOfWork = foUnitOfWork;
        }
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Divistion/DivistionList.cshtml");
        }
        public IActionResult Detail(Guid id)
        {
            Division loDivision = new Division();
            if (id != Guid.Empty)
            {
                loDivision = moUnitOfWork.DivisionRepository.GetDivision(id);
            }
            loDivision.ZoneList = moUnitOfWork.ZoneRepository.GetZoneDropDown();
            return View("~/Areas/Admin/Views/Divistion/DivisionDetail.cshtml",loDivision);
        }
        public IActionResult SaveDivision(Division foDivision)
        {
            try
            {
                int liSuccess = 0;
                int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()); //User.FindFirst(SessionConstant)
                if (foDivision != null)
                {
                    moUnitOfWork.DivisionRepository.SaveDivision(foDivision, liUserId, out liSuccess);
                    if (liSuccess == (int)CommonFunctions.ActionResponse.Add)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                        TempData["Message"] = string.Format(AlertMessage.RecordAdded, "Division");
                        return RedirectToAction("Index");
                    }
                    else if (liSuccess == (int)CommonFunctions.ActionResponse.Update)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Update;
                        TempData["Message"] = string.Format(AlertMessage.RecordUpdated, "Division");
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                        TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving division");
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving division");
                return RedirectToAction("Index");
            }
        }
        public IActionResult GetDivisionList(string DivisionName, int? Status, int? sort_column, string sort_order, int? pg, int? size)
        {
            StringBuilder lolog = new StringBuilder();
            try
            {
                lolog.AppendLine("DivisionName : " + DivisionName);
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

                List<DivisionListResult> loDivisionListResults = new List<DivisionListResult>();
                loDivisionListResults = moUnitOfWork.DivisionRepository.GetAllDivision(DivisionName == null ? DivisionName : DivisionName.Trim(), Status, sort_column, sort_order, pg.Value, size.Value);
                dynamic loModel = new ExpandoObject();
                loModel.GetDivisionList = loDivisionListResults;
                if (loDivisionListResults.Count > 0)
                {
                    liTotalRecords = loDivisionListResults[0].inRecordCount;
                    liStartIndex = loDivisionListResults[0].inRownumber;
                    liEndIndex = loDivisionListResults[loDivisionListResults.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/Admin/Views/Divistion/_DivistionListData.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }

        }
    }
}
