using CMSBAL.Desk.Models;
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
using System.Text;
using static CMSUtility.Utilities.CommonConstant;

namespace FileSystemWeb.Areas.Divisions.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Divisions")]
    public class DeskController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;
        public DeskController(IUnitOfWork foUnitOfWork)
        {
            moUnitOfWork = foUnitOfWork;
        }
        public IActionResult Index()
        {
            return View("~/Areas/Divisions/Views/Desk/DeskList.cshtml");
        }
        public IActionResult Detail(Guid id)
        {
            Desk loDesk = new Desk();
            if (id != Guid.Empty)
            {
                loDesk = moUnitOfWork.DeskRepository.GetDesk(id);
            }
            loDesk.ZoneList = moUnitOfWork.ZoneRepository.GetZoneDropDown();
            //loDesk.DivisionList = moUnitOfWork.DivisionRepository.GetDivisionDropDown();
            loDesk.DepartmentList = moUnitOfWork.DepartmentRepository.GetDepartmentDropDown();
            //loDesk.DesignationList = moUnitOfWork.DesignationRepository.GetDesignationDropDown(loDesk.inDepartmentId);
            //return View("~/Areas/Admin/Views/Divistion/DivisionDetail.cshtml", loDivision);
            return View("~/Areas/Divisions/Views/Desk/DeskDetail.cshtml", loDesk);
        }
        public IActionResult SaveDesk(Desk foDesk)
        {
            try
            {
                int liSuccess = 0;
                int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()); //User.FindFirst(SessionConstant)
                if (foDesk != null)
                {
                    moUnitOfWork.DeskRepository.SaveDesk(foDesk, liUserId, out liSuccess);
                    if (liSuccess == (int)CommonFunctions.ActionResponse.Add)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                        TempData["Message"] = string.Format(AlertMessage.RecordAdded, "Desk");
                        return RedirectToAction("Index");
                    }
                    else if (liSuccess == (int)CommonFunctions.ActionResponse.Update)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Update;
                        TempData["Message"] = string.Format(AlertMessage.RecordUpdated, "Desk");
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                        TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving desk");
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving desk");
                return RedirectToAction("Index");
            }
        }

        public IActionResult GetDeskList(string DeskName, int? Status, int? sort_column, string sort_order, int? pg, int? size)
        {
            StringBuilder lolog = new StringBuilder();
            try
            {
                lolog.AppendLine("DeskName : " + DeskName);
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

                List<DeskListResult> loDeskListResults = new List<DeskListResult>();
                loDeskListResults = moUnitOfWork.DeskRepository.GetDeskList(DeskName == null ? DeskName : DeskName.Trim(), Status, sort_column, sort_order, pg.Value, size.Value,Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()));
                dynamic loModel = new ExpandoObject();
                loModel.GetDeskList = loDeskListResults;
                if (loDeskListResults.Count > 0)
                {
                    liTotalRecords = loDeskListResults[0].inRecordCount;
                    liStartIndex = loDeskListResults[0].inRownumber;
                    liEndIndex = loDeskListResults[loDeskListResults.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/Divisions/Views/Desk/_DeskListData.cshtml", loModel);
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
        public IActionResult GetDesignationDropDown(int fiDepartmentId)
        {
            List<Select2> DesignationDropDown = moUnitOfWork.DesignationRepository.GetDesignationDropDown(fiDepartmentId);
            return Json(new { data = DesignationDropDown });

        }

    }
}
