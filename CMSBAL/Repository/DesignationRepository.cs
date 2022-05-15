using CMSBAL.Data;
using CMSBAL.Designation.Models;
using CMSBAL.Repository.IRepository;
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
    public class DesignationRepository : IDesignationRepository
    {
        private readonly DatabaseContext moDatabaseContext;

        public DesignationRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }

        public void DeleteDepartment(Guid fuDesignationId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC deleteDesignation @unDesignationId={fuDesignationId}, @inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }

        public List<DesignationListResult> GetDesignationList(string stDesignationName, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize,int? fiUserId = null)
        {
            return moDatabaseContext.Set<DesignationListResult>().FromSqlInterpolated($"EXEC getDesignationList @stDesignationName={stDesignationName},  @inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder}, @inPageNo={fiPageNo},@inPageSize={fiPageSize},@inUserId={fiUserId}").ToList();
        }

        public Designation.Models.Designation GetDesignation(Guid fuDesignationId)
        {
            return moDatabaseContext.Set<Designation.Models.Designation>().FromSqlInterpolated($"EXEC getDesignationDetail @unDesignationId={fuDesignationId}").AsEnumerable().FirstOrDefault();
        }

        public void SaveDesignation(Designation.Models.Designation foDesignation, int fiUserId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveDesignation @inDesignationId={foDesignation.inDesignationId},@stDesignationName={foDesignation.stDesignationName},@inZoneId={foDesignation.inZoneId},@inDivisionId={foDesignation.inDivisionId},@inDepartmentId={foDesignation.inDepartmentId},@inCreatedBy={fiUserId},@inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }

        public List<Select2> GetDesignationDropDown(int fiDepartmentId)
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getDesignationDropDown @inDepartmentId={fiDepartmentId}").ToList();
        }
    }
}
