using CMSBAL.Repository.IRepository;
using CMSBAL.User.Models;
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

namespace FileSystemWeb.Areas.DeskAdmin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("DeskAdmin")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork moUnitOfWork;
        private readonly static int miPageSize = 10;
        public UserController(IUnitOfWork foUnitOfWork)
        {
            moUnitOfWork = foUnitOfWork;
        }
        public IActionResult Index()
        {
            return View("~/Areas/DeskAdmin/Views/User/UserList.cshtml");
        }

        public IActionResult Detail(Guid id)
        {
            UserProfile loUserProfile = new UserProfile();
            if (id != Guid.Empty)
            {
                loUserProfile = moUnitOfWork.UserRepository.GetUserDetail(id);
            }
            loUserProfile.DeskList = moUnitOfWork.DeskRepository.GetDeskDropDown(Convert.ToInt32(User.FindFirst(SessionConstant.DivisionId).Value.ToString()));
            loUserProfile.DepartmentList = moUnitOfWork.DepartmentRepository.GetDepartmentDropDown();
            loUserProfile.inZoneId = Convert.ToInt32(User.FindFirst(SessionConstant.ZoneId).Value.ToString());
            loUserProfile.inDivisionId = Convert.ToInt32(User.FindFirst(SessionConstant.DivisionId).Value.ToString());
            loUserProfile.inStoreId = Convert.ToInt32(User.FindFirst(SessionConstant.StoreId).Value.ToString());
            loUserProfile.DesignationList = moUnitOfWork.DesignationRepository.GetDesignationDropDown(Convert.ToInt32(User.FindFirst(SessionConstant.DepartmentId).Value.ToString()));
            return View("~/Areas/DeskAdmin/Views/User/UserDetail.cshtml", loUserProfile);
        }
        public IActionResult SaveUserProfile(UserProfile foUserProfile)
        {
            try
            {
                int liSuccess = 0;
                int liUserId = Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value.ToString()); //User.FindFirst(SessionConstant)
                foUserProfile.inDepartmentId = Convert.ToInt32(User.FindFirst(SessionConstant.DepartmentId).Value);
                if (foUserProfile != null)
                {
                    moUnitOfWork.UserRepository.InserUserProfile(foUserProfile, liUserId, out liSuccess);
                    if (liSuccess == (int)CommonFunctions.ActionResponse.Add)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                        TempData["Message"] = string.Format(AlertMessage.RecordAdded, "User");
                        return RedirectToAction("Index");
                    }
                    else if (liSuccess == (int)CommonFunctions.ActionResponse.Update)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Update;
                        TempData["Message"] = string.Format(AlertMessage.RecordUpdated, "User");
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                        TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving user");
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                TempData["Message"] = string.Format(AlertMessage.OperationalError, "saving user");
                return RedirectToAction("Index");
            }
        }
        public IActionResult GetUserList(string UserName, int? Status, int? sort_column, string sort_order, int? pg, int? size)
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

                List<UserListResult> loUserListResults = new List<UserListResult>();
                int liDivisionId = Convert.ToInt32(User.FindFirst(SessionConstant.DivisionId).Value.ToString());
                int liDepartmentId = Convert.ToInt32(User.FindFirst(SessionConstant.DepartmentId).Value.ToString());
                loUserListResults = moUnitOfWork.UserRepository.GetUserList(liDepartmentId, liDivisionId, UserName == null ? UserName : UserName.Trim(), sort_column, sort_order, pg.Value, size.Value, Convert.ToInt32(User.FindFirst(SessionConstant.Id).Value));
                dynamic loModel = new ExpandoObject();
                loModel.GetUserList = loUserListResults;
                if (loUserListResults.Count > 0)
                {
                    liTotalRecords = loUserListResults[0].inRecordCount;
                    liStartIndex = loUserListResults[0].inRownumber;
                    liEndIndex = loUserListResults[loUserListResults.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Areas/DeskAdmin/Views/User/_UserList.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }

        }

    }
}
