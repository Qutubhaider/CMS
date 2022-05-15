using CMSBAL.Case.Models;
using CMSBAL.Repository.IRepository;
using CMSUtility.Service.PaginationService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using static CMSUtility.Utilities.CommonConstant;

namespace FileSystemWeb.Areas.DeskAdmin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("DeskAdmin")]
    public class CaseController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;
        public CaseController(IUnitOfWork foUnitOfWork)
        {
            moUnitOfWork = foUnitOfWork;
        }
        public IActionResult Index()
            {
                return View("~/Areas/DeskAdmin/Views/Case/CaseList.cshtml");
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
                return PartialView("~/Areas/DeskAdmin/Views/Case/_CaseList.cshtml", loModel);
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
            return View("~/Areas/DeskAdmin/Views/Case/CaseDetail.cshtml", loCaseDetail);
        }
        
    }
}
