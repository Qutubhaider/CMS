using CMSBAL.Department.Models;
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
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;
        public DepartmentController(IUnitOfWork foUnitOfWork)
        {
            moUnitOfWork = foUnitOfWork;
        }
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Department/DepartmentList.cshtml");
        }
        public IActionResult Detail(Guid id)
        {
            Department loDepartment = new Department();
            if(id != Guid.Empty)
            {
                loDepartment = moUnitOfWork.DepartmentRepository.GetDepartment(id);
            }
            return View("~/Areas/Admin/Views/Department/DepartmentDetail.cshtml",loDepartment);
        }
        public IActionResult SaveDepartment(Department foDepartment)
        {
            try
            {
                int liSuccess = 0;
                int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()); //User.FindFirst(SessionConstant)
                if (foDepartment != null)
                {
                    moUnitOfWork.DepartmentRepository.SaveDepartment(foDepartment, liUserId, out liSuccess);
                    if (liSuccess == (int)CommonFunctions.ActionResponse.Add)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                        TempData["Message"] = string.Format(AlertMessage.RecordAdded, "Department");
                        return RedirectToAction("Index");
                    }
                    else if (liSuccess == (int)CommonFunctions.ActionResponse.Update)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Update;
                        TempData["Message"] = string.Format(AlertMessage.RecordUpdated, "Department");
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                        TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving department");
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving department");
                return RedirectToAction("Index");
            }
        }
        public IActionResult GetDepartmentList(string DepartmentName, int? sort_column, string sort_order, int? pg, int? size)
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

                List<DepartmentListResult> loDepartmentListResult = new List<DepartmentListResult>();
                loDepartmentListResult = moUnitOfWork.DepartmentRepository.GetDepartmentList(DepartmentName == null ? DepartmentName : DepartmentName.Trim(), sort_column, sort_order, pg.Value, size.Value);
                dynamic loModel = new ExpandoObject();
                loModel.GetDepartmentList = loDepartmentListResult;
                if (loDepartmentListResult.Count > 0)
                {
                    liTotalRecords = loDepartmentListResult[0].inRecordCount;
                    liStartIndex = loDepartmentListResult[0].inRownumber;
                    liEndIndex = loDepartmentListResult[loDepartmentListResult.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/Admin/Views/Department/_DepartmentListData.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }

        }
    }
}
