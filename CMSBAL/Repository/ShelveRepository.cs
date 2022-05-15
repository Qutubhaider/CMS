using CMSBAL.Data;
using CMSBAL.Repository.IRepository;
using CMSBAL.Shelve.Models;
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
    public class ShelveRepository : IShelveRepository
    {
        private readonly DatabaseContext moDatabaseContext;

        public ShelveRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }

        public void DeleteShelve(Guid fuShelveId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC deleteShelve @unShelveId={fuShelveId}, @inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }

        public Shelve.Models.Shelve GetShelveDetail(Guid fuShelveId)
        {
            return moDatabaseContext.Set<Shelve.Models.Shelve>().FromSqlInterpolated($"EXEC getShelveDetail @unShelveId={fuShelveId}").AsEnumerable().FirstOrDefault();
        }

        public List<Select2> GetShelveDropDown(int fiAlmirahId)
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getShelveDropDown @inAlmirahId={fiAlmirahId}").ToList();
        }

        public List<ShelveListResult> GetShelveList(string fsShelveNumber, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null)
        {
            return moDatabaseContext.Set<ShelveListResult>().FromSqlInterpolated($"EXEC getShelveList @stShelveNumber={fsShelveNumber}, @inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder}, @inPageNo={fiPageNo},@inPageSize={fiPageSize},@inUserId={fiUserId}").ToList();
        }

        public void SaveShelve(Shelve.Models.Shelve foShelve, int fiUserId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveShelve @inShelveId={foShelve.inShelveId},@stShelveNumber={foShelve.stShelveNumber}, @inAlmirahId={foShelve.inAlmirahId} ,@inStoreId={foShelve.inStoreId},@inZoneId={foShelve.inZoneId},@inDivisionId={foShelve.inDivisionId},@inDepartmentId={foShelve.inDepartmentId},@inRoomId={foShelve.inRoomId},@inCreatedBy={fiUserId},@inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }
    }
}
