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
    public class ZoneRepository : IZoneRepository
    {
        private readonly DatabaseContext moDatabaseContext;

        public ZoneRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }
        public void DeleteZone(Guid fuZoneId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC deleteZone @unZoneId={fuZoneId}, @inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }

        public Zone.Models.Zone GetZoneDetail(Guid fuZoneId)
        {
            return moDatabaseContext.Set<Zone.Models.Zone>().FromSqlInterpolated($"EXEC getZoneDetail @unZoneId={fuZoneId}").AsEnumerable().FirstOrDefault();
        }

        public List<Select2> GetZoneDropDown()
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getZoneDropDown").ToList();
        }

        public List<Zone.Models.ZoneListResult> GetZoneList(string fsZoneName, int? finStatus, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null)
        {
            return moDatabaseContext.Set<Zone.Models.ZoneListResult>().FromSqlInterpolated($"EXEC getZoneList @stZoneName={fsZoneName}, @inStatus={finStatus}, @inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder}, @inPageNo={fiPageNo},@inPageSize={fiPageSize},@inUserId={fiUserId}").ToList();
        }

        public void SaveZone(Zone.Models.Zone foZone, int fiUserId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveZone @inZoneId={foZone.inZoneId},@stZoneName={foZone.stZoneName},@inCreatedBy={fiUserId},@inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }
    }
}
