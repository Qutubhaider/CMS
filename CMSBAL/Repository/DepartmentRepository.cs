using CMSBAL.Data;
using CMSBAL.Department.Models;
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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DatabaseContext moDatabaseContext;

        public DepartmentRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }
        public void DeleteDepartment(Guid fuDepartmentId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC deleteDepartment @unDepartmentId={fuDepartmentId}, @inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }

        public Department.Models.Department GetDepartment(Guid fuDepartmentId)
        {
            return moDatabaseContext.Set<Department.Models.Department>().FromSqlInterpolated($"EXEC getDepartmentDetail @unDepartmentId={fuDepartmentId}").AsEnumerable().FirstOrDefault();
        }

        public List<Select2> GetDepartmentDropDown()
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getDepartmentDropDown").ToList();
        }

        public List<DepartmentListResult> GetDepartmentList(string fsDepartmentName, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize,int? fiUserId=null)
        {
            return moDatabaseContext.Set<DepartmentListResult>().FromSqlInterpolated($"EXEC getDepartmentList @stDepartmentName={fsDepartmentName},  @inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder}, @inPageNo={fiPageNo},@inPageSize={fiPageSize},@inUserId={fiUserId}").ToList();
        }

        public void SaveDepartment(Department.Models.Department foDepartment, int fiUserId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveDepartment @inDepartmentId={foDepartment.inDepartmentId},@stDepartmentName={foDepartment.stDepartmentName},@inCreatedBy={fiUserId},@inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }
    }
}
