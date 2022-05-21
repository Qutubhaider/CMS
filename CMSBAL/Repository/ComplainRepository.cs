using CMSBAL.Complain;
using CMSBAL.Complain.Models;
using CMSBAL.Data;
using CMSBAL.Repository.IRepository;
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
    public class ComplainRepository : IComplainRepository
    {
        private readonly DatabaseContext moDatabaseContext;
        private int fiSuccess;

        public ComplainRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }
        public void SaveComplain(Complain.Models.Complain foComplain, int fiZoneId, int fiDivisionId, int fiUserId,int fiStoreId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveComplain @inDepartmentId={foComplain.inDepartmentId}, @inCategoryId={foComplain.inCategoryId}, @inSubCategoryId={foComplain.inSubCategoryId}, @stComplainText={foComplain.stComplainText}, @stUnFileName={foComplain.stUnFileName}, @stFileName={foComplain.stFileName}, @inZoneId={fiZoneId}, @inDivisionId={fiDivisionId}, @inStoreId={fiStoreId}, @inUserId={fiUserId}, @inSuccess={loSuccess} OUT ");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }
        public List<ComplainListResultUser> GetComplainList(string complain, int? finStatus, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize)
        {
            return moDatabaseContext.Set<ComplainListResultUser>().FromSqlInterpolated($"EXEC getComplainList @stComplain={complain}, @inStatus={finStatus},@inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder},@inPageNo={fiPageNo},@inPageSize={fiPageSize}").ToList();
        }
    }
}
