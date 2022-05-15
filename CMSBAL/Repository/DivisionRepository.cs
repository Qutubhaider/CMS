using CMSBAL.Data;
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
    public class DivisionRepository : IDivisionRepository
    {
        private readonly DatabaseContext moDatabaseContext;

        public DivisionRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }
        public List<Select2> GetDivisionDropDown(int fiZoneId)
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getDivisionDropDown @inZoneId={fiZoneId}").ToList();
        }
        public void DeleteDivision(Guid fuDivisionId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC deleteDivision @unDivisionId={fuDivisionId}, @inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }

        public List<Division.Models.DivisionListResult> GetAllDivision(string fsDivisionTitle, int? finStatus, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null)
        {
            return moDatabaseContext.Set<Division.Models.DivisionListResult>().FromSqlInterpolated($"EXEC getDivisionList @stDivistionName={fsDivisionTitle}, @inStatus={finStatus}, @inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder}, @inPageNo={fiPageNo},@inPageSize={fiPageSize},@inUserId={fiUserId}").ToList();
        }

        public Division.Models.Division GetDivision(Guid fuDivisionId)
        {
            return moDatabaseContext.Set<Division.Models.Division>().FromSqlInterpolated($"EXEC getDivisionDetail @unDivisionId={fuDivisionId}").AsEnumerable().FirstOrDefault();
        }

        public void SaveDivision(Division.Models.Division foDivision, int fiUserId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveDivision @inDivisionId={foDivision.inDivisionId},@inZoneId={foDivision.inZoneId},@stDivisionName={foDivision.stDivisionName},@inCreatedBy={fiUserId},@inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }
    }
}
