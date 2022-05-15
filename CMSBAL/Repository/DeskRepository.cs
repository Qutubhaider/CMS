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
    public class DeskRepository:IDeskRepository
    {
        private readonly DatabaseContext moDatabaseContext;

        public DeskRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }
        public void DeleteDesk(Guid fuDivisionId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC deleteDesk @unDeskId={fuDivisionId}, @inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }

        public List<Desk.Models.DeskListResult> GetDeskList(string fsDeskTitle, int? finStatus, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null)
        {
            return moDatabaseContext.Set<Desk.Models.DeskListResult>().FromSqlInterpolated($"EXEC getDeskList @stDeskName={fsDeskTitle}, @inStatus={finStatus}, @inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder}, @inPageNo={fiPageNo},@inPageSize={fiPageSize},@inUserId={fiUserId}").ToList();
        }

        public Desk.Models.Desk GetDesk(Guid fuDeskId)
        {
            return moDatabaseContext.Set<Desk.Models.Desk>().FromSqlInterpolated($"EXEC getDeskDetail @unDeskId={fuDeskId}").AsEnumerable().FirstOrDefault();
        }

        public void SaveDesk(Desk.Models.Desk foDesk, int fiUserId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveDeskDetail @inDeskId={foDesk.inDeskId},@stDeskName={foDesk.stDeskName},@inZoneId={foDesk.inZoneId},@inDivisionId={foDesk.inDivisionId},@inDepartmentId={foDesk.inDepartmentId},@inDesignationId={foDesk.inDesignationId},@inStatus={foDesk.inStatus},@inCreatedBy={fiUserId},@inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }
        public List<Select2> GetDeskDropDown(int fiDivisionId)
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getDeskDropDown @inDivisionId={fiDivisionId}").ToList();
        }

    }
}
