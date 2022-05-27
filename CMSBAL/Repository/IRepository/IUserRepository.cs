using CMSBAL.User.Models;
using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
   public interface IUserRepository
    {
        public User.Models.UserEmailResult GetUserByEmail(string stEmail);
        public void InserUserProfile(User.Models.UserProfile foUserProfile, int fiUserId, out int fiSuccess);
        User.Models.UserProfile GetUserDetail(Guid fuUserId);
        void DeleteUser(Guid fuUserId, out int fiSuccess);
        List<User.Models.UserListResult> GetUserList(int? fiDepartmentId, int? fiDivisionId, string fsUserName, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize,int? fiUserId=null);
        List<Select2> GetUserDropDown();
        List<Select2> GetUserListForIssueFile(int fiStoreId,int inDivisionId);
        List<Select2> GetUserListByDivisionId(int fiDivisionId);
        List<Select2> GetUserListByDepartmentId(int fiDepartmentId);
        UserDropDownDetailResult GetUserDetailFromDropDown(int fiUserId);
        void SaveUser(UserRegisterVM fouser,out int success);
        MyProfile GetUserProfile(Guid unUserId);
        void SaveUserProfile(MyProfile foUser, out int fiSuccess);
        void UpdateNewPassword(int inUserId, string newPassword, out int fiSuccess);
    }
}
