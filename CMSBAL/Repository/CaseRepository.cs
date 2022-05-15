using CMSBAL.Case.Models;
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
    public class CaseRepository : ICaseRepository
    {
        private readonly DatabaseContext moDatabaseContext;
        private int fiSuccess;

        public CaseRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }
        public void DeleteCase(Guid fuCaseId, out int fiSuccess)
        {
            throw new NotImplementedException();
        }

        public void SaveCase(Case.Models.Case foCase, int fiUserId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC acceptAssignFile @inIssueFileId={foCase.inIssueFileId}, @inZoneId={foCase.inZoneId},@inDivisionId={foCase.inDivisionId},@inDepartmentId={foCase.inDepartmentId},@inDesignationId={foCase.inDesignationId},@inStoreFileDetailId={foCase.inStoreFileDetailId},@inStatus={1},@stComment={foCase.stComment},@inAcceptedBy={fiUserId},@inAssignedBy={foCase.inAssignedBy},@inSRId={foCase.inSRId},@inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);

        }
        public List<CaseListResult> GetCaseList(string fsFileName, int inStatus, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null)
        {
           return moDatabaseContext.Set<CaseListResult>().FromSqlInterpolated($"EXEC getCaseList @stFileName={fsFileName},@inStatus={inStatus},  @inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder}, @inPageNo={fiPageNo},@inPageSize={fiPageSize},@inUserId={fiUserId}").ToList();
        }
        public CaseDetailResult GetCaseDetail(Guid fuCaseId)
        {
            return moDatabaseContext.Set<CaseDetailResult>().FromSqlInterpolated($"EXEC getCaseDetail @unCaseId={fuCaseId}").AsEnumerable().FirstOrDefault();
        }
    }
}
