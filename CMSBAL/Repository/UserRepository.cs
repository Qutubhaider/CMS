using CMSBAL.Data;
using CMSBAL.Repository.IRepository;
using CMSBAL.Store.Models;
using CMSBAL.User.Models;
using CMSUtility.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly DatabaseContext moDatabaseContext;

        public UserRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }

        public void DeleteUser(Guid fuUserId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC deleteUser @unUserId={fuUserId}, @inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }

        public List<Select2> GetUserDropDown()
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getUserDropDown").ToList();
        }
        public List<Select2> GetUserListForIssueFile(int fiStoreId,int inDivisionId)
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getUserListForIssueFile @inStoreId={fiStoreId},@inDivisionId={inDivisionId}").ToList();
        }
        public UserEmailResult GetUserByEmail(string stEmail)
        {
            return moDatabaseContext.Set<UserEmailResult>().FromSqlInterpolated($"EXEC getUserByEmail @stUserEmail={stEmail}").AsEnumerable().FirstOrDefault();
        }

        public UserProfile GetUserDetail(Guid fuUserId)
        {
            return moDatabaseContext.Set<UserProfile>().FromSqlInterpolated($"EXEC getUserDetail @unUserProfileId={fuUserId}").AsEnumerable().FirstOrDefault();
        }

        public List<UserListResult> GetUserList(int? fiDepartmentId,int? fiDivisionId, string fsStoreName, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize,int? fiUserId=null)
        {
            return moDatabaseContext.Set<UserListResult>().FromSqlInterpolated($"EXEC getUserList @inDepartmentId={fiDepartmentId} ,@inDivisionId={fiDivisionId}, @stUserName={fsStoreName}, @inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder}, @inPageNo={fiPageNo},@inPageSize={fiPageSize},@inUserId={fiUserId}").ToList();
        }

        public void InserUserProfile(UserProfile foUserProfile, int fiUserId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC insertUserProfile @inUserProfileId={foUserProfile.inUserProfileId},@inDeskid={foUserProfile.inDeskid},@inUserId={foUserProfile.inUserId},@inRoleId={foUserProfile.inRole},@inZoneId={foUserProfile.inZoneId},@inStoreId={foUserProfile.inStoreId},@inDivisionId={foUserProfile.inDivisionId},@inDepartmentId={foUserProfile.inDepartmentId},@inDesignationId={foUserProfile.inDesignationId},@stFirstName={foUserProfile.stFirstName},@stLastName={foUserProfile.stLastName},@stEmail={foUserProfile.stEmail},@stMobile={foUserProfile.stMobile},@stAddress={foUserProfile.stAddress},@inStatus={foUserProfile.inStatus}, @inCreatedBy ={fiUserId},@inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }

        public List<Select2> GetUserListByDivisionId(int fiDivisionId)
        {

            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getUserListByDivisionId @inDivisionId={fiDivisionId}").ToList();

        }

        public List<Select2> GetUserListByDepartmentId(int fiDepartmentId)
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getUserListDepartmentId @inDepartmentId={fiDepartmentId}").ToList();
        }

        public UserDropDownDetailResult GetUserDetailFromDropDown(int fiUserId)
        {
            return moDatabaseContext.Set<UserDropDownDetailResult>().FromSqlInterpolated($"EXEC getUserDataByDropDown @inUserId={fiUserId}").AsEnumerable().FirstOrDefault();
        }

        public void SaveUser(UserRegisterVM foUser, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveUserDetails @inDeskid={foUser.inDeskid},@inZoneId={foUser.inZoneId},@inStoreId={foUser.inStoreId},@inDivisionId ={foUser.inDivisionId},@inDepartmentId  ={foUser.inDepartmentId},@inDesignationId ={foUser.inDesignationId},@stFirstName={foUser.stFirstName},@stLastName ={foUser.stLastName},@stEmail={foUser.stEmail},@stMobile={foUser.stMobile},@stAddress ={foUser.stAddress},@inEmployeeType={foUser.inEmployeeType},@stPFNumber ={foUser.stPFNumber},@stEmployeeNumber={foUser.stEmployeeNumber},@stPPONumber ={foUser.stPPONumber},@inStatus={1}, @inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }
    }
}
